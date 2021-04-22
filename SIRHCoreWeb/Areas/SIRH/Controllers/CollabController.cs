using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIRHCoreData.Models.DB;

namespace SIRHCoreWeb.Areas.SIRH.Controllers
{
    [Area("SIRH")]
    [Route("SIRH/[controller]/[action]")]
    public class CollabController : Controller
    {
        private DB_SIRHContext db = new DB_SIRHContext();

        public IActionResult Colab()
        {

            return View();
        }

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