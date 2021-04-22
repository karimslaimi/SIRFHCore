using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIRHCoreData.Models.DB;
using Newtonsoft.Json;

namespace SIRHCoreWeb.Areas.SIRH.Pages
{
    public class CollaborateursModel : PageModel
    {
        private DB_SIRHContext db = new DB_SIRHContext();

        public void OnGet()
        {
        }

        public IActionResult OnGetSelectDepartements()
        {
            IEnumerable<TblDimDepartement> user = db.TblDimDepartement.ToList();
            //string requestJson = JsonConvert.SerializeObject(user);
            return new JsonResult(user);
        }

        public IActionResult OnGetlistcolab()
        {
            List<TblDwSalarie> ls = db.TblDwSalarie.ToList();
            TblDwSalarie x = db.TblDwSalarie.FirstOrDefault(x => x.Matricule == 222);
            return new JsonResult(ls);
        }

        public IActionResult OnGetSelectResponsables()
        {
            List<TblDwSalarie> responsable = db.TblDwSalarie.Where(x => x.IdFonction == 4 || x.IdFonction == 5).ToList();

            return new JsonResult(responsable);
        }
    }
}
