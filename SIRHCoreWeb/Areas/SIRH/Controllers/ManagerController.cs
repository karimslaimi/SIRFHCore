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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SIRHCoreWeb.Areas.SIRH.Controllers
{
    [Area("SIRH")]
    [Route("SIRH/[controller]/[action]")]
    [Authorize(Roles = "Responsable RH")]
    public class ManagerController : Controller


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

        public ManagerController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            service = new CongeService();
            servicef = new FraisService();
            personneService = new PersonneService();
            projetService = new ProjetService();
            tachesService = new TachesService();

        }
        ICongeService service = null;
        IFraisService servicef = null;



        IProjetService projetService = null;
        IPersonneService personneService = null;
        ITachesService tachesService = null;
        /************************************/


        [HttpGet]
        public ActionResult addProject()
        {
            Projet projet = new Projet();

            return View(projet);
        }
        [HttpPost]
        public ActionResult addProject(Projet projet)
        {
            ModelState.Remove("createur");
            if (ModelState.IsValid)
            {

                string name = User.Identity.Name;

                try
                {

                    projet.datedeb = DateTime.Now;
                    Personne personne = personneService.Get(x => x.UserName == name);
                    if (personne.Projets == null)
                    {
                        personne.Projets = new List<Projet>();
                    }
                    personne.Projets.Add(projet);
                    personneService.Update(personne);

                }
                catch (Exception e)
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

            if (_project != null)
            {

                projetService.Delete(x => x.id == id);

            }
            return Redirect("projects");
        }


        [HttpGet]
        public ActionResult EditProject(long id)
        {
            Projet projet = projetService.Get(x => x.id == id);

            if (projet != null)
            {
                var collabs = userManager.GetUsersInRoleAsync("Collaborateur").Result;
                ViewBag.collabs = collabs.Select(x =>
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


        public ActionResult affectCollab(string username, int id, string rss)
        {
            if (id == 0)
            {
                if (!string.IsNullOrEmpty(rss))
                {
                    return RedirectToAction("DetailProject", new { id = id });
                }
                return RedirectToAction("Projects");
            }
            if (string.IsNullOrEmpty(username))
            {
                if (!string.IsNullOrEmpty(rss))
                {
                    return RedirectToAction("DetailProject", new { id = id });
                }
                return RedirectToAction("EditProject", new { id = id });
            }
            Projet projet = projetService.Get(x => x.id == id);
            Personne personne = personneService.Get(x => x.UserName == username);
            if (projet == null || personne == null)
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
                return RedirectToAction("DetailProject", new { id = id });
            }


            return RedirectToAction("EditProject", new { id = id });
        }

        public ActionResult RemoveCollab(int projid, string userid, string rss)
        {
            if (projid == 0 || userid == "")
            {
                return RedirectToAction("Projects");
            }
            CollaborateurService collaborateurService = new CollaborateurService();
            collaborateurService.Delete(x => x.Personne.Id == userid.ToString() && x.Projet.id == projid);

            if (string.IsNullOrEmpty(rss))
            {

                return RedirectToAction("EditProject", new { id = projid });
            }
            else
            {
                return RedirectToAction("DetrailProject", new { id = projid });
            }

        }


        public ActionResult DetailProject(int id)
        {
            Projet projet = projetService.GetProjet(id);
            CollaborateurService collaborateurService = new CollaborateurService();
            ViewBag.collab = personneService.GetMany(x => x.Collaborations.Where(s => s.Projet.id == id).Any());
            var collabs = userManager.GetUsersInRoleAsync("Collaborateur").Result;
            ViewBag.persons = collabs.Select(x =>
                   new SelectListItem { Value = x.UserName.ToString(), Text = x.UserName }

            ).ToList();
           
            
            
            TachesService tachesService = new TachesService();
            ViewBag.taches = tachesService.GetTachesbyProjct(id);

            return View(projet);
        }





        [HttpGet]
        public ActionResult addTask(int projid)
        {
            Taches taches = new Taches();
            ViewBag.project = projid;
            return View(taches);
        }

        public ActionResult addTask(Taches taches,int projid)
        {
            ModelState.Remove("creator");
            ModelState.Remove("Projet");
            if (ModelState.IsValid)
            {
                string name = User.Identity.Name;
                taches.Projet = projetService.Get(x => x.id == projid);
                Personne personne= personneService.Get(x => x.UserName == name);

               
                 
             
                taches.date = DateTime.Now;
                taches.state = "pending";



                if (personne.Taches == null)
                {
                    personne.Taches = new List<Taches>();

                }
                personne.Taches.Add(taches);
                personneService.Update(personne);

                return RedirectToAction("DetailProject", new { id = projid });


            }
            else
            {
                ViewBag.project = projid;

                return View(taches);
            }
        }



        public ActionResult finishTask(int id)
        {
            Taches taches = tachesService.GetTaches(id);
            taches.state = "finished";
            tachesService.Update(taches);
            return RedirectToAction("DetailProject", new { id = taches.Projet.id });
        }









        //********************************Liste des utilisateurs**************************//

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }
        //**************************** toutes les demandes d'un demandeur X******************//
        [HttpGet]
        public IActionResult Lesdemandes(string id,string search)
        {
            if (search == null)
            {
                var Conges = service.GetCongeB(id);

                return View(Conges);
            }
            else
            {
                var Conges = service.GetCongeByStatut(search);
                return View(Conges);
            }
            
        }
        //*******************traitement d'une demande*********************//

        public IActionResult Traitement(int id)
        {

            Conge con = service.GetConge(id);
            return View(con);

        }
        /***************************valider ou refuser *********************/
       
        public IActionResult Reponse(int id)
        {
           
            Conge conn = service.GetConge(id);
          
            return View(conn);

        }
        [HttpPost]

        public IActionResult Reponse(Conge conn, Solde s)
        {
         //conn.Userid = conn.Userid;
            conn.RH =this. User.Identity.Name;

            if (ModelState.IsValid)
            {

                var sol = service.GetSoldeByUser(s.Userid).LastOrDefault();
               
                service.traiterConge(conn);

                s.Userid = conn.Userid;
                if (sol == null)
                { 
                    double annuel = 0;
                double maladie = 0;
                double maternite = 0;
                 double sanssolde = 0;
                if ((conn.Statut == "Accepter") && (conn.type == "Annuel"))

                {
                    s.Annuel = annuel + conn.Duree;
                    s.Maladie = maladie;
                    s.Maternité = maternite;
                    s.SansSolde = sanssolde;
                    //s.Annuel = s.Annuel+conn.;
                }

                else if ((conn.Statut == "Accepter") && (conn.type == "Maladie"))
                {
                    s.Annuel = annuel;
                    s.Maladie = maladie + conn.Duree;
                    s.Maternité = maternite;
                    s.SansSolde = sanssolde;
                    }

                else if ((conn.Statut == "Accepter") && (conn.type == "Maternité"))
                {
                    s.Annuel = annuel;
                    s.Maladie = maladie;
                    s.Maternité = maternite + conn.Duree;
                    s.SansSolde = sanssolde;
                    }
                    else if ((conn.Statut == "Accepter") && (conn.type == "Sans solde"))
                    {
                        s.Annuel = annuel;
                        s.Maladie = maladie;
                        s.Maternité = maternite;
                        s.SansSolde = sanssolde + conn.Duree;
                    }
                    else
                {
                    s.Annuel = annuel;
                    s.Maladie = maladie;
                    s.Maternité = maternite;
                    s.SansSolde = sanssolde;
                    }
            }



            else if (sol != null)
                {
                    double annuel = service.GetSoldeByUser(conn.Userid).LastOrDefault().Annuel;
                    double maladie = service.GetSoldeByUser(conn.Userid).LastOrDefault().Maladie;
                    double maternite = service.GetSoldeByUser(conn.Userid).LastOrDefault().Maternité;
                    double sanssolde = service.GetSoldeByUser(conn.Userid).LastOrDefault().SansSolde;
                    if ((conn.Statut == "Accepter") && (conn.type == "Annuel"))

                    {
                        s.Annuel = annuel + conn.Duree;
                        s.Maladie = maladie;
                        s.Maternité = maternite;
                        s.SansSolde = sanssolde;
                        //s.Annuel = s.Annuel+conn.;
                    }

                    else if ((conn.Statut == "Accepter") && (conn.type == "Maladie"))
                    {
                        s.Annuel = annuel;
                        s.Maladie = maladie + conn.Duree;
                        s.Maternité = maternite;
                        s.SansSolde = sanssolde;
                    }

                    else if ((conn.Statut == "Accepter") && (conn.type == "Maternité"))
                    {
                        s.Annuel = annuel;
                        s.Maladie = maladie;
                        s.Maternité = maternite + conn.Duree;
                        s.SansSolde = sanssolde;
                    }
                    else if ((conn.Statut == "Accepter") && (conn.type == "Sans solde"))
                    {
                        s.Annuel = annuel;
                        s.Maladie = maladie;
                        s.Maternité = maternite;
                        s.SansSolde = sanssolde + conn.Duree;
                    }
                    else
                    {
                        s.Annuel = annuel;
                        s.Maladie = maladie;
                        s.Maternité = maternite;
                        s.SansSolde = sanssolde;
                    }
                }
                service.solde(s);
                return RedirectToAction("ListUsers");
            }



            return View(conn);
        }
        //**************************** toutes les Notes de frais d'un demandeur X******************//
        [HttpGet]
        public IActionResult Lesfrais(string id)
        {

            var Frais = servicef.GetFraisB(id);

            return View(Frais);

        }

        /***************************valider ou refuser note de frais *********************/

        public IActionResult Reponsefrais(int id)
        {

            NoteDeFrais frais= servicef.GetFrais(id);

            return View(frais);

        }
        [HttpPost]

        public IActionResult Reponsefrais(NoteDeFrais frais)
        {
            //conn.Userid = conn.Userid;
            frais.RH = this.User.Identity.Name;

            if (ModelState.IsValid)
            {
                //conn.Userid = userManager.Users;
                //service.GetCongeB(id);
                servicef.traiterFrais(frais);

                return RedirectToAction("ListUsers");
            }
            return View(frais);
        }

        /**********solde****************/
        [HttpGet]
        public IActionResult consltSolde(string id)
        {

            

            Solde solde = service.GetSoldeB(id).LastOrDefault();
           
            return View(solde);


        }
    }
}