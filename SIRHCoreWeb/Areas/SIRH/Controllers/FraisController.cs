using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIRHCoreDomain;
using SIRHCoreService;
using System.Data;
using Microsoft.AspNetCore.Authorization;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SIRHCoreWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace SIRHCoreWeb.Areas.SIRH.Controllers
{

    [Area("SIRH")]
    [Route("SIRH/[controller]/[action]")]
    [Authorize(Roles = "Collaborateur")]

    public class FraisController : Controller
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                service.Dispose();
            }
            base.Dispose(disposing);
        }

        private readonly UserManager<IdentityUser> userManager;

        public FraisController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            service = new FraisService();
        }
        IFraisService service = null;

        /*******************************************************/


        [Authorize]
        public IActionResult CreateN()

        {

            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateN(NoteDeFrais f)


        {
            f.Userid = this.User.Identity.Name;
             const string statut = "à valider";

            if (ModelState.IsValid)
            {

                f.Userid = this.User.Identity.Name;
                 f.Statut = statut;

                service.CreateFrais(f);


                return RedirectToAction("Index");

            }
            return View(f);
        }
        //********************Index******************//
        [Authorize]
        public IActionResult Index(string search)
        {
            if (search == null)
            {
                string user = User.Identity.Name;
                var Conges = service.GetFraisByUser(user);

                return View(Conges);
            }
            else
            {
                var Conges = service.GetFraisByStatut(search);
                return View(Conges);
            }

        }
        //************Edit********************//
        [Authorize]
        public IActionResult Edit(int id)
        {
            NoteDeFrais c = service.GetFrais(id);
            return View(c);

        }

        [HttpPost]

        public IActionResult Edit(NoteDeFrais f)
        {

            //const string statut = "à valider";
        

            if (ModelState.IsValid)
            {
                //c.Statut = statut;
                f.Userid = User.Identity.Name;
                if (f.Statut == "à valider")
                {
                    service.UpdateFrais(f);

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "la demande est déjà validée, vous ne pouvez pas la modifier";

                    return View();
                }
            }

            return View(f);
        }


        /////*********************Delete************************//

        [Authorize]
        public IActionResult Delete(int id)
        {

            NoteDeFrais con = service.GetFrais(id);
            return View(con);
        }
        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteFrais(int id)

        {

            try
            {
                if (ModelState.IsValid)
                {
                    NoteDeFrais conn = service.GetFrais(id);
                    service.DeleteFrais(conn);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View();
        }
        ///***************************Details**************************///

        public IActionResult Details(int id)
        {

            NoteDeFrais conn = service.GetFrais(id);

            return View(conn);

        }






    }
}