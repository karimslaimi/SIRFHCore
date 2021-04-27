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

            //the controller constructor to instantiate services and inject the usermanager and role manager dependecies


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

            //this is the get method to return the view
            Projet projet = new Projet();

            return View(projet);
        }
        [HttpPost]
        public  ActionResult addProject(Projet projet)
        {
            ModelState.Remove("createur");
            if (ModelState.IsValid)
            {
                //as we have a relation between the client and the reclam we have to get the client who created the reclam and put him in the reclam

                string name = User.Identity.Name;

                try
                {
                    //today datatime
                    projet.datedeb = DateTime.Now;
                    Personne personne = personneService.Get(x => x.UserName == name);
                    //a problem occured when we tried to add the projet from the project repository so we decided to add it from the personne repository
                    //as the person have a list of project we add the project to this list
                    if (personne.Projets == null)
                    {//if the list is null (wasn't fetched or the user don't have any project
                        //we create a new array list of projects
                        personne.Projets = new List<Projet>();
                    }
                    //add the project to the list of projectgs
                    personne.Projets.Add(projet);
                    //update the user
                    personneService.Update(personne);

                }
                catch(Exception e)
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

        public ActionResult projects(string search)
        {
            //project list
            List<Projet> _projects = null;
            //check if the filter is null that means we need to fetch all projects
            if (string.IsNullOrEmpty(search))
            {
                //getall
            _projects = projetService.GetAll().ToList();

            }
            else
            {
                //filter
                _projects = projetService.GetMany(x => x.nom.Contains(search) || x.description.Contains(search)).ToList();
            }

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
            //get method to return the view
            //get the project by id
            Projet projet = projetService.Get(x=>x.id==id);
         
            //check if it exists cause a user may manipulate the url 
            if (projet != null)
            {
                //all collaborators list and put it in the viewbag

                var collabs = userManager.GetUsersInRoleAsync("Collaborateur").Result;
                ViewBag.collabs=collabs.Select(x=> 
                     new SelectListItem { Value = x.UserName.ToString(), Text = x.UserName }

                ).ToList();
                //get the collaborators who are in this project
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
            {//submit edit
                projetService.Update(_projet);
               
                return Redirect("projects");
            }
            else
            {
                return View(_projet);
            }
    

        }


        public ActionResult affectCollab(string username,int id,string rss)
        {
            //this method we need the username of the collab the id of the project and the rss to check from where the request came whether from edit page or the detail page


            if (id == 0)
            {//if project id is null (equal to 0) 
                
                return RedirectToAction("Projects");
            }
            if (string.IsNullOrEmpty(username))
            {
                if (!string.IsNullOrEmpty(rss))
                {//redirect to detail page
                    return RedirectToAction("DetailProject", new { id = id });
                }
                //redirect to edit project page
                return RedirectToAction("EditProject", new { id = id });
            }
            
            Projet projet = projetService.Get(x=>x.id==id);
            Personne personne = personneService.Get(x => x.UserName == username);


            if(projet==null || personne == null)
            {
                if (!string.IsNullOrEmpty(rss))
                {
                    return RedirectToAction("DetailProject", new { id = id });
                }
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


            if (!string.IsNullOrEmpty(rss))
            {
                return RedirectToAction("DetailProject", new { id = id});
            }
            return RedirectToAction("EditProject", new { id = id});
        }

        public ActionResult RemoveCollab(int projid, string userid,string rss)
        {
            //user or project are empty (the user may manipulate the request and it will raise an error
            if(projid==0 || userid =="")
            {
                return RedirectToAction("Projects" );
            }
            
            CollaborateurService collaborateurService = new CollaborateurService();
            collaborateurService.Delete(x => x.Personne.Id == userid.ToString() && x.Projet.id == projid);

            //whther the request came from the detail or edit page
            if (string.IsNullOrEmpty(rss))
            {

            return RedirectToAction("EditProject", new { id = projid});

            }
            else
            {
                return RedirectToAction("DetailProject", new { id = projid });
            }

        }


        public ActionResult DetailProject(int id)
        {

            //get the project with its relations
            Projet projet = projetService.GetProjet(id);
            //get the collaborators who collaborate in the project
            CollaborateurService collaborateurService = new CollaborateurService();
            ViewBag.collab = personneService.GetMany(x => x.Collaborations.Where(s=>s.Projet.id==id).Any());
          
            //the usermanager is async method we need to user .Result to fetch the data
            var collabs = userManager.GetUsersInRoleAsync("Collaborateur").Result;
           
            //create a dropdown list containning all the collaborators 
            //the value is the username and the text also is the username
            //you can change the text to be the name of the collab
            ViewBag.persons = collabs.Select(x =>
                   new SelectListItem { Value = x.UserName.ToString(), Text = x.UserName }

            ).ToList();
            TachesService tachesService = new TachesService();
            //get tasks of this project
            ViewBag.taches = tachesService.GetTachesbyProjct(id).ToList();

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