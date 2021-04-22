using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SIRHCoreService;
using SIRHCoreDomain;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using SIRHCoreWeb.Data;
using SIRHCoreWeb.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace SIRHCoreWeb.Areas.SIRH.Controllers
{
    [Area("SIRH")]
    [Route("SIRH/[controller]/[action]")]
    [Authorize(Roles = "Administrateur")]
    public class AdminController : Controller
    {

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                service.Dispose();
            }
            base.Dispose(disposing);
        }
        public readonly UserManager<IdentityUser> userManager;
        public readonly RoleManager<IdentityRole> roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;

            service = new CongeService();


            servicef = new FraisService();
            projetService = new ProjetService();
            personneService = new PersonneService();
        }


        ICongeService service = null;
        IFraisService servicef = null;

        IProjetService projetService = null;
        IPersonneService personneService = null;
        /************************************/


        [HttpGet]
        public ActionResult addProject()
        {
            Projet projet = new Projet();

            return View(projet);
        }
        [HttpPost]
        public async Task<ActionResult> addProject(Projet projet)
        {
            ModelState.Remove("createur");
            if (ModelState.IsValid)
            {
                //as we have a relation between the client and the reclam we have to get the client who created the reclam and put him in the reclam

                string name = User.Identity.Name;

                try
                {
                    projet.createurId = personneService.Get(x => x.UserName == name).Id;

                projet.datedeb = DateTime.Now;
                projetService.Add(projet);
               

                }catch(Exception e)
                {
                    Console.WriteLine(e);
                }

            
            }
            else
            {
                return View(projet);
            }
            return Redirect("projects");


        }

        public ActionResult projects()
        {

            List<Projet> _projects = projetService.GetAll().ToList();
            return View(_projects);

        }




        public ActionResult deleteProject(int id)
        {

            Projet _project = projetService.GetById(id);
            
            if (_project != null )
            {

                projetService.Delete(x => x.id == id);
                
            }
            return Redirect("projects");
        }


        [HttpGet]
        public ActionResult EditProject(long id)
        {
            Projet projet = projetService.Get(x=>x.id==id);
         
            if (projet != null)
            {
                var collabs = userManager.GetUsersInRoleAsync("Collaborateur").Result;
                ViewBag.collabs=collabs.Select(x=> 
                     new SelectListItem { Value = x.UserName.ToString(), Text = x.UserName }

                ).ToList();

                ViewBag.collaborateur = personneService.GetMany(x => x.Collaborations.Where(s => s.Projet.id == id).Any()).ToList();

                return View(projet);
            }
            else
            {
                return Redirect("projects");
            }
        }


       


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditProject(Projet _projet)
        {

            if (ModelState.IsValid)
            {
                projetService.Update(_projet);
               
                return Redirect("projects");
            }
            else
            {
                return View(_projet);
            }
    

        }


        public ActionResult affectCollab(string username,int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Projects");
            }
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("EditProject", new { id = id });
            }
            Projet projet = projetService.Get(x=>x.id==id);
            Personne personne = personneService.Get(x => x.UserName == username);
            if(projet==null || personne == null)
            {
                return RedirectToAction("EditProject", new { id = id });
            }
            Collaboration collaboration = new Collaboration();
            collaboration.Personne = personne;
            collaboration.Projet = projet;
            if (personne.Collaborations == null)
            {
                personne.Collaborations = new List<Collaboration>();
            }
            personne.Collaborations.Add(collaboration);
            personneService.Update(personne);
          
            return RedirectToAction("EditProject", new { id = id });
        }

        public ActionResult RemoveCollab(int projid, string userid)
        {
            if(projid==0 || userid =="")
            {
                return RedirectToAction("Projects");
            }
            CollaborateurService collaborateurService = new CollaborateurService();
            collaborateurService.Delete(x => x.Personne.Id == userid.ToString() && x.Projet.id == projid);
            return RedirectToAction("EditProject", new { id = projid });
        }


        public ActionResult DetailProject(int id)
        {
            Projet projet = projetService.GetProjet(id);
            return View(projet);
        }







        /*********************************/

        public ActionResult RoleAddToUser()
        {
            // prepopulat roles for the view dropdown
            var list = roleManager.Roles.OrderBy(r => r.Name).ToList().Select(rr =>

          new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            var listt = userManager.Users.OrderBy(r => r.UserName).ToList().Select(rr =>

          new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
            ViewBag.Users = listt;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ManageUserRolesAsync(string UserName, string RoleName)
        {
            if ((UserName != null) && (RoleName != null))
            {
                var user = await userManager.FindByNameAsync(UserName);
                //  var user = userManager.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                //var account = new AdminController();

                var roleresult = await userManager.AddToRoleAsync(user, RoleName);


                ViewBag.ResultMessages = "Rôle est attribué à cet utilisateur avec succès!";
            }
            else
            { ViewBag.ResultMessages = "selectionnez un utilisateur ."; }
            // prepopulat roles for the view dropdown
            var list = roleManager.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            var listt = userManager.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
            ViewBag.Users = listt;

            return View("RoleAddToUser");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetRolesAsync(string UserName)
        {
            if (UserName == null)
            { ViewBag.ResultMessagess = "selectionnez un utilisateur."; }
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                var user = await userManager.FindByNameAsync(UserName);
               
                // IdentityUser user = userManager.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                ViewBag.RolesForThisUser = await userManager.GetRolesAsync(user);

                // prepopulat roles for the view dropdown
                var list = roleManager.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
                var listt = userManager.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
                ViewBag.Users = listt;
            }

            return View("RoleAddToUser");
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRoleForUserAsync(string UserName, string RoleName)

        {
            if ((UserName != null) && (RoleName != null))
            {
                var user = await userManager.FindByNameAsync(UserName);
                //  ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                if (await userManager.IsInRoleAsync(user, RoleName))
                {
                    await userManager.RemoveFromRoleAsync(user, RoleName);
                    ViewBag.ResultMessage = "Rôle est supprimé de cet utilisateur avec succès !";
                }
                else
                {
                    ViewBag.ResultMessage = "Cet utilisateur n'appartient pas au rôle sélectionné.";
                }
            }
            else
            { ViewBag.ResultMessage = "selectionnez un utilisateur ."; }
            // prepopulat roles for the view dropdown
            var list = roleManager.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            var listt = userManager.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
            ViewBag.Users = listt;

            return View("RoleAddToUser");
        }









    }
}