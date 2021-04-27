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
            incidentService = new IncidentService();
        }
        ICongeService service = null;
        IFraisService servicef = null;



        IProjetService projetService = null;
        IPersonneService personneService = null;
        ITachesService tachesService = null;
        IIncidentService incidentService = null;
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

        public ActionResult projects(string search)
        {
            List<Projet> _projects = null;

            if (string.IsNullOrEmpty(search))
            {
                _projects = projetService.GetAll().ToList();

            }
            else
            {
                _projects = projetService.GetMany(x => x.nom.Contains(search) || x.description.Contains(search)).ToList();
            }

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
                return RedirectToAction("DetailProject", new { id = projid });
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
            //remove it so that the modelstate will be valid
            ModelState.Remove("creator");
            ModelState.Remove("Projet");
            if (ModelState.IsValid)
            {
                //get the authenticated user username
                string name = User.Identity.Name;
                //get the project and assign it to the task we are going to create
                taches.Projet = projetService.Get(x => x.id == projid);
                Personne personne= personneService.Get(x => x.UserName == name);

               
                 
             
                taches.date = DateTime.Now;
                taches.state = "En cours";


                //the personne won't allow us to create the task without passing by the personne
                //we get the personne we add a task to him w update the personne that way task will be saved
                if (personne.Taches == null)
                {
                    personne.Taches = new List<Taches>();

                }
                //we add a task to a user and the upadte the user
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
            //update the task and make it closed
            Taches taches = tachesService.GetTaches(id);
            taches.state = "Terminé";
            tachesService.Update(taches);
            return RedirectToAction("DetailProject", new { id = taches.Projet.id });
        }






        [HttpGet]
        public ActionResult addIncident()
        {
            Incident incident = new Incident();
            return View(incident);
        }


        [HttpPost]
        public ActionResult addIncident(Incident incident)
        {
            if (ModelState.IsValid)
            {
                incident.DateCreation = DateTime.Now;
                incident.status = "En cours";
                //get the authenticated user and assign an incident to him
                //identity won't allow us to add an entity without passing by it
                string name = User.Identity.Name;
                Personne personne = personneService.Get(x => x.UserName == name);
                if (personne.Incidents == null)
                {
                    personne.Incidents = new List<Incident>();
                }
                //add the incident to the list of user's incident and update the user
                personne.Incidents.Add(incident);
                personneService.Update(personne);
                return RedirectToAction("incidents");

            }
            else
            {
                return View(incident);
            }
        }


        public ActionResult incidents(bool mine,string search)
        {
            //incidents list with and without filter
            List<Incident> incidents = null;
            if (mine == true)
            {
                string name = User.Identity.Name;
                incidents = incidentService.GetUserIncident(name).ToList();
                if (!string.IsNullOrWhiteSpace(search))
                {
                    incidents = incidents.Where(x => x.Description.Contains(search) || x.status.Contains(search) || x.title.Contains(search)).ToList();
                }
            }
            else
            {
                incidents = incidentService.GetIncident().ToList();
               
                if (!string.IsNullOrWhiteSpace(search))
                {
                    incidents = incidents.Where(x => x.Description.Contains(search) || x.status.Contains(search) || x.title.Contains(search)).ToList();
                }
            }

            //the manager will have the collaborators list and can assign an incident to a collab
            var collabs = userManager.GetUsersInRoleAsync("Collaborateur").Result;
            //creating a dropdown list 
            ViewBag.collabs = collabs.Select(x =>
                   new SelectListItem { Value = x.UserName.ToString(), Text = x.UserName }

            ).ToList();

            return View(incidents);
        }


        public ActionResult deleteIncident(int id)
        {
            incidentService.Delete(x => x.Id == id);
            return RedirectToAction("incidents");
        }


        [HttpGet]
        public ActionResult editIncident(int id)
        {
            Incident incident = incidentService.Get(x => x.Id == id);
            return View(incident);

        }

        public ActionResult editIncident(Incident incident)
        {
            if (ModelState.IsValid)
            {

                incidentService.Update(incident);

                return RedirectToAction("incidents");

            }
            else
            {
                return View(incident);
            }
        }



        public ActionResult affectIncident(int incid, string username)
        {
            Incident incident1 = incidentService.Get(i => i.Id == incid);
            Personne personne = personneService.Get(x => x.UserName == username);
            if (personne.traitements == null)
            {
                personne.traitements = new List<Incident>();
            }

            personne.traitements.Add(incident1);
            personneService.Update(personne);

            return RedirectToAction("Incidents");
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