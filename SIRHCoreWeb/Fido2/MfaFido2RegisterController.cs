using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fido2NetLib.Objects;
using Fido2NetLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Fido2NetLib.Fido2;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIRHCoreWeb
{

    [Route("api/[controller]")]
    public class MfaFido2RegisterController : Controller
    {
        [TempData]
        public string UniqueId { get; set; }
        [TempData]
        public string[] RecoveryCodes { get; set; }

        private Fido2 _lib;
        public static IMetadataService _mds;
        private string _origin;
        private readonly Fido2Storage _fido2Storage;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDistributedCache _distributedCache;

        public MfaFido2RegisterController(IConfiguration config, Fido2Storage fido2Storage, UserManager<IdentityUser> userManager, IDistributedCache distributedCache)
        {
            _userManager = userManager;
            _fido2Storage = fido2Storage;
            _distributedCache = distributedCache;
            var MDSAccessKey = config["fido2:MDSAccessKey"];
            var MDSCacheDirPath = config["fido2:MDSCacheDirPath"] ?? Path.Combine(Path.GetTempPath(), "fido2mdscache");
            _mds = string.IsNullOrEmpty(MDSAccessKey) ? null : MDSMetadata.Instance(MDSAccessKey, MDSCacheDirPath);
            if (null != _mds)
            {
                if (false == _mds.IsInitialized())
                    _mds.Initialize().Wait();
            }
            _origin = config["fido2:origin"];
            if (_origin == null)
            {
                _origin = "https://localhost:44386";
            }

            var domain = config["fido2:serverDomain"];
            if (domain == null)
            {
                domain = "localhost";
            }

            _lib = new Fido2(new Fido2Configuration()
            {
                ServerDomain = domain,
                ServerName = "Fido2MfaTest",
                Origin = _origin,
                // Only create and use Metadataservice if we have an acesskey
                MetadataService = _mds,
                TimestampDriftTolerance = config.GetValue<int>("fido2:TimestampDriftTolerance")
            });
        }

        private string FormatException(Exception e)
        {
            return string.Format("{0}{1}", e.Message, e.InnerException != null ? " (" + e.InnerException.Message + ")" : "");
        }

        [HttpPost]
        [Route("/makeCredentialOptions")]
        public async Task<JsonResult> MakeCredentialOptions([FromForm] string username, [FromForm] string displayName, [FromForm] string attType, [FromForm] string authType, [FromForm] bool requireResidentKey, [FromForm] string userVerification)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    username = $"{displayName} (Usernameless user created at {DateTime.UtcNow})";
                }

                var identityUser = await _userManager.FindByEmailAsync(username);
                var user = new Fido2User
                {
                    DisplayName = identityUser.UserName,
                    Name = identityUser.UserName,
                    Id = Encoding.UTF8.GetBytes(identityUser.UserName) // byte representation of userID is required
                };

                // 2. Get user existing keys by username
                var items = await _fido2Storage.GetCredentialsByUsername(identityUser.UserName);
                var existingKeys = new List<PublicKeyCredentialDescriptor>();
                foreach (var publicKeyCredentialDescriptor in items)
                {
                    existingKeys.Add(publicKeyCredentialDescriptor.Descriptor);
                }

                // 3. Create options
                var authenticatorSelection = new AuthenticatorSelection
                {
                    RequireResidentKey = requireResidentKey,
                    UserVerification = userVerification.ToEnum<UserVerificationRequirement>()
                };

                if (!string.IsNullOrEmpty(authType))
                    authenticatorSelection.AuthenticatorAttachment = authType.ToEnum<AuthenticatorAttachment>();

                var exts = new AuthenticationExtensionsClientInputs() { Extensions = true, UserVerificationIndex = true, Location = true, UserVerificationMethod = true, BiometricAuthenticatorPerformanceBounds = new AuthenticatorBiometricPerfBounds { FAR = float.MaxValue, FRR = float.MaxValue } };

                var options = _lib.RequestNewCredential(user, existingKeys, authenticatorSelection, attType.ToEnum<AttestationConveyancePreference>(), exts);

                // 4. Temporarily store options, session/in-memory cache/redis/db
                //HttpContext.Session.SetString("fido2.attestationOptions", options.ToJson());
                var uniqueId = Guid.NewGuid().ToString();
                UniqueId = uniqueId;
                await _distributedCache.SetStringAsync(uniqueId, options.ToJson());

                // 5. return options to client
                return Json(options);
            }
            catch (Exception e)
            {
                return Json(new CredentialCreateOptions { Status = "error", ErrorMessage = FormatException(e) });
            }
        }

        [HttpPost]
        [Route("/makeCredential")]
        public async Task<JsonResult> MakeCredential([FromBody] AuthenticatorAttestationRawResponse attestationResponse)
        {
            try
            {
                // 1. get the options we sent the client
                //var jsonOptions = HttpContext.Session.GetString("fido2.attestationOptions");
                var jsonOptions = await _distributedCache.GetStringAsync(UniqueId);
                if (string.IsNullOrEmpty(jsonOptions))
                {
                    throw new Exception("Can't get CredentialOptions from cache");
                }
                var options = CredentialCreateOptions.FromJson(jsonOptions);

                // 2. Create callback so that lib can verify credential id is unique to this user
                IsCredentialIdUniqueToUserAsyncDelegate callback = async (IsCredentialIdUniqueToUserParams args) =>
                {
                    var users = await _fido2Storage.GetUsersByCredentialIdAsync(args.CredentialId);
                    if (users.Count > 0) return false;

                    return true;
                };

                // 2. Verify and make the credentials
                var success = await _lib.MakeNewCredentialAsync(attestationResponse, options, callback);

                // 3. Store the credentials in db
                await _fido2Storage.AddCredentialToUser(options.User, new FidoStoredCredential
                {
                    Username = options.User.Name,
                    Descriptor = new PublicKeyCredentialDescriptor(success.Result.CredentialId),
                    PublicKey = success.Result.PublicKey,
                    UserHandle = success.Result.User.Id,
                    SignatureCounter = success.Result.Counter,
                    CredType = success.Result.CredType,
                    RegDate = DateTime.Now,
                    AaGuid = success.Result.Aaguid
                });

                // 4. return "ok" to the client
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Json(new CredentialMakeResult { Status = "error", ErrorMessage = $"Unable to load user with ID '{_userManager.GetUserId(User)}'." });
                }

                await _userManager.SetTwoFactorEnabledAsync(user, true);
                if (await _userManager.CountRecoveryCodesAsync(user) == 0)
                {
                    var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                    RecoveryCodes = recoveryCodes.ToArray();
                }

                return Json(success);
            }
            catch (Exception e)
            {
                return Json(new CredentialMakeResult { Status = "error", ErrorMessage = FormatException(e) });
            }
        }
    }
}