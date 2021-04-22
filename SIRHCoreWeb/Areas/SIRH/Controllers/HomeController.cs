using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SIRHCoreData;
using SIRHCoreDomain;
using SIRHCoreWeb.Data;

namespace SIRHCoreWeb.Areas.SIRH.Controllers
{
    [Area("SIRH")]
    [Route("SIRH/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly SIRHcontext _context;
        public readonly IPasswordHasher<IdentityUser> _passwordHasher;
        public readonly UserManager<IdentityUser> userManager;
        public readonly RoleManager<IdentityRole> roleManager;

        public HomeController(IPasswordHasher<IdentityUser> _passwordHasher, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;

         
            this._passwordHasher = _passwordHasher;

        }
        public IActionResult Index()
        {



            return View();
        }
    }
}