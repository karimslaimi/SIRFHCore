using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SIRHCoreData;
using SIRHCoreDomain;
using SIRHCoreService;
using SIRHCoreWeb.Models;

namespace SIRHCoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SIRHcontext _context;
        public readonly IPasswordHasher<IdentityUser> _passwordHasher;
        public readonly UserManager<IdentityUser> userManager;
        public readonly RoleManager<IdentityRole> roleManager;

        public HomeController(IPasswordHasher<IdentityUser> _passwordHasher, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<HomeController> logger)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;

            _logger = logger;
            this._passwordHasher = _passwordHasher;

        }
 

        public async Task<IActionResult> Index()
        {
            Personne applicationUser = new Personne();
            PersonneService service = new PersonneService();
            //applicationUser.UserName = "karimmaanger";
            //applicationUser.NormalizedUserName = "KARIMNAANGER";
            //applicationUser.Email = "karimnar@live.fr";
            //applicationUser.NormalizedEmail = "KARIMNAR@LIVE.FR";
            //applicationUser.CIN = 11451243;
            //applicationUser.Nom = "karim";
            //applicationUser.Prenom = "karim";
            //applicationUser.Adresse = "tunis";
            //applicationUser.EmailConfirmed = true;
            //applicationUser.PhoneNumber = "51887898";

            //applicationUser.DateDeNais = "03-03-2000";
            //applicationUser.PasswordHash = _passwordHasher.HashPassword(applicationUser, "Karim.nar123");
            //
            // service.Add(applicationUser);
            // var user =  userManager.FindByEmailAsync("karimnar@live.fr").Result;

            // var roleresult =  userManager.AddToRoleAsync(user, "Responsable RH");


            //applicationUser.UserName = "karimcolab";
            //applicationUser.NormalizedUserName = "KARIMCOLAB";
            //applicationUser.Email = "karim-nar1@live.fr";
            //applicationUser.NormalizedEmail = "KARIM-NAR1@LIVE.FR";
            //applicationUser.CIN = 11451243;
            //applicationUser.Nom = "karim";
            //applicationUser.Prenom = "karim";
            //applicationUser.Adresse = "tunis";
            //applicationUser.EmailConfirmed = true;
            //applicationUser.PhoneNumber = "51887898";

            //applicationUser.DateDeNais = "03-03-2000";
            //applicationUser.DateDeNais = "03-03-2000";
            //applicationUser.PasswordHash = _passwordHasher.HashPassword(applicationUser, "Karim.nar123");
            //service.Add(applicationUser);
            //var user = userManager.FindByEmailAsync("karim-nar1@live.fr").Result;
            //var roleresult = userManager.AddToRoleAsync(user, "Collaborateur");

            // applicationUser.UserName = "karimadmin";
            // applicationUser.NormalizedUserName = "KARIMADMIN";
            // applicationUser.Email = "karim-nar@live.fr";
            // applicationUser.NormalizedEmail = "KARIM-NAR@LIVE.FR";
            // applicationUser.CIN = 11451243;
            // applicationUser.Nom = "karim";
            // applicationUser.Prenom = "karim";
            // applicationUser.Adresse = "tunis";
            // applicationUser.EmailConfirmed = true;
            // applicationUser.PhoneNumber = "51887898";

            // applicationUser.DateDeNais = "03-03-2000";
            // applicationUser.PasswordHash = _passwordHasher.HashPassword(applicationUser, "Karim.nar123");
            // PersonneService service = new PersonneService();
            // service.Add(applicationUser);





            //// var _val = await userManager.CreateAsync(applicationUser, "Karim.123");

            // var user = await userManager.FindByNameAsync("karimadmin");
            // var roleresult = await userManager.AddToRoleAsync(user, "Administrateur");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
