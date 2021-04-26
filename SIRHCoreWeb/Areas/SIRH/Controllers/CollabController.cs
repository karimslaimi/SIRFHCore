using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIRHCoreData.Models.DB;
using SIRHCoreDomain;
using SIRHCoreService;

namespace SIRHCoreWeb.Areas.SIRH.Controllers
{
    [Area("SIRH")]
    [Route("SIRH/[controller]/[action]")]
    public class CollabController : Controller
    {
        public CollabController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            personneService = new PersonneService();
            projetService = new ProjetService();
            tachesService = new TachesService();
            incidentService = new IncidentService();
        }

        private readonly UserManager<IdentityUser> userManager;
        IProjetService projetService = null;
        IPersonneService personneService = null;
        ITachesService tachesService = null;
        IIncidentService incidentService = null;


        private DB_SIRHContext db = new DB_SIRHContext();

        public IActionResult Colab()
        {

            return View();
        }




        /********************************/



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



        public ActionResult myProjects(string search)
        {
            List<Projet> _projects = null;
            string name = User.Identity.Name;
            if (string.IsNullOrEmpty(search))
            {
                _projects = projetService.GetMany(x=>x.createur.UserName==name).ToList();

            }
            else
            {
                _projects = projetService.GetMany(x =>x.createur.UserName==name&& (x.nom.Contains(search) || x.description.Contains(search))).ToList();
            }

            return View(_projects);
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
            string name = User.Identity.Name;
            ViewBag.iscollab = personneService.GetMany(x =>x.UserName==name && x.Collaborations.Where(s => s.Projet.id == id).Any()).Any();

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

        public ActionResult addTask(Taches taches, int projid)
        {
            ModelState.Remove("creator");
            ModelState.Remove("Projet");
            if (ModelState.IsValid)
            {
                string name = User.Identity.Name;
                taches.Projet = projetService.Get(x => x.id == projid);
                Personne personne = personneService.Get(x => x.UserName == name);




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




        /********************************/








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
                string name = User.Identity.Name;
                Personne personne = personneService.Get(x => x.UserName == name);
                if (personne.Incidents == null)
                {
                    personne.Incidents = new List<Incident>();
                }
                personne.Incidents.Add(incident);
                personneService.Update(personne);
                return RedirectToAction("incidents");

            }
            else
            {
                return View(incident);
            }
        }


        public ActionResult mesIncident( string search)
        {
            
            List<Incident> incidents = null;
            
         
                string name = User.Identity.Name;
                incidents = incidentService.GetUserIncident(name).ToList();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    incidents = incidents.Where(x => x.Description.Contains(search) || x.status.Contains(search) || x.title.Contains(search)).ToList();
                }
         
 
                 

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

        //i have to test these methods and add mark as treated


        public ActionResult markAsTreated(int incid)
        {
            Incident incident1 = incidentService.Get(i => i.Id == incid);
            incident1.DateReglage = DateTime.Now;
            incident1.status = "Traité";
            incidentService.Update(incident1);

            return RedirectToAction("Incidents");
        }






        /********************************/





        public IActionResult ListColaborateur()
        {
            IEnumerable<TblDwSalarie> LS = db.TblDwSalarie.ToList();
            return View(LS);
        }

        public JsonResult listcolab()
        {
            List<TblDwSalarie> ls = db.TblDwSalarie.ToList();
            TblDwSalarie x = db.TblDwSalarie.FirstOrDefault(x => x.Matricule == 222);
            return Json(ls);
        }
        public IActionResult OnGetSelectDepartements()
        {
            IEnumerable<TblDimDepartement> user = db.TblDimDepartement.ToList();

            return new JsonResult(user);
        }

        public JsonResult GetResponsable()
        {
            List<TblDwSalarie> responsable = db.TblDwSalarie.Where(x => x.IdFonction == 4 || x.IdFonction == 5).ToList();

            return Json(responsable);
        }



        public IActionResult Index()
        {
            return View();
        }
        public bool InsertUser(string civilite, string name, string prenom, int matricule, int departement, string phone)
        {
            TblDwSalarie salarie = new TblDwSalarie();
            salarie.Civilite = civilite;
            salarie.Nom = name;
            salarie.Prenom = prenom;
            salarie.Email = "AAA";
            salarie.Tel = phone;
            salarie.Matricule = matricule;
            salarie.IdFonction = 1;
            salarie.IdStructure = 1;
            try
            {
                db.TblDwSalarie.Add(salarie);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertColab(string civilite, string name, string prenom, int matricule, int departement, string phone)
        {
            TblDwSalarie salarie = new TblDwSalarie();

            salarie.Civilite = civilite;
            salarie.Nom = name;
            salarie.Prenom = prenom;
            salarie.Email = "AAA";
            salarie.Tel = phone;
            salarie.Matricule = matricule;
            salarie.IdFonction = 1;
            salarie.IdStructure = 1;
            try
            {
                db.TblDwSalarie.Add(salarie);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}