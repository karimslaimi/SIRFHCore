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
    public class CongeController : Controller
    {

       public double Soldeannuel =25;
       public const double Soldemaladie = 5;
        public const double Soldematernite = 76;
        


        ICongeService service = null;
        public CongeController()
        {
            service = new CongeService();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                service.Dispose();
            }
            base.Dispose(disposing);
        }
        //**************************Create***********************************//
       [Authorize]
        public IActionResult Create()
        {
            
            return View();
        }
       
        [Authorize]
        [HttpPost]

        public IActionResult Create(Conge c,Solde s)


        {
            c.Userid = this.User.Identity.Name;
            const string statut = "à valider";
     
            if (ModelState.IsValid)
            {

                c.Userid =this.User.Identity.Name;
                s.Userid = this.User.Identity.Name;
                c.Statut = statut;
                double t = (c.DateFin - c.DateDeb).Value.TotalDays;
                c.Duree = t;   
                var con = service.GetCongeByUser(c.Userid).LastOrDefault();
                var sol = service.GetSoldeByUser(s.Userid).LastOrDefault();

                if (con == null)

                    {  if(c.type=="Sans solde")
                         { service.CreateConge(c); }
                    if ((c.type=="Annuel")&& (c.Duree <= Soldeannuel))
                         { service.CreateConge(c); }
                      else if ((c.type == "Annuel") && (c.Duree > Soldeannuel))
                      {
                        //TempData["msg"] = "<script>alert('Vous avez depassé ton solde du congé annuel');</script>";
                        TempData["msg"] = "Vous avez depassé ton solde du congé annuel";
                        return View();
                       }
                       if ((c.type == "Maladie")&& (c.Duree <= Soldemaladie))
                         { service.CreateConge(c); }
                       else if ((c.type == "Maladie") && (c.Duree > Soldemaladie))
                       {

                        //TempData["msg"] = "<script>alert('Vous avez depassé ton solde du congé de maladie');</script>";
                        TempData["msg"] = "Vous avez depassé ton solde du congé de maladie";

                        return View();
                    }
                        if ((c.type == "Maternite") &&(c.Duree <= Soldematernite))
                       { service.CreateConge(c); }
                        else if ((c.type == "Maternite") && (c.Duree > Soldematernite))
                        {

                        // TempData["msg"] = "<script>alert('Vous avez depassé ton solde du congé de maternité');</script>";
                        TempData["msg"] = "Vous avez depassé ton solde du congé de maternité";

                        return View();
                        }

                }




                    else if ((con != null)&&(sol==null))
                    {

                    double annuel = 0; 
                    double maladie =0;
                    double maternite =0;
                   
                    double validmal= service.GetCongeByUser(c.Userid).Where(x => x.Statut == "à valider").Where(x => x.type == "Maladie").Sum(x => x.Duree);
                    double validmat = service.GetCongeByUser(c.Userid).Where(x => x.Statut == "à valider").Where(x => x.type == "Maternité").Sum(x => x.Duree);
                    double valideann = service.GetCongeByUser(c.Userid).Where(x => x.Statut == "à valider").Where(x => x.type == "Annuel").Sum(x => x.Duree);
                    if (c.type == "Sans solde")
                    { service.CreateConge(c); }
                    if (((c.Duree + annuel+valideann) > Soldeannuel) && (c.type == "Annuel"))
                        {


                     // TempData["msg"] = "<script>alert('Vous avez depassé ton solde du congé annuel');</script>";
                        //c.Annuel = annuel;
                        TempData["msg"] = "Vous avez depassé ton solde du congé annuel";
                        // return View();


                    }
                    


                    else if (((c.Duree + annuel+valideann) <= Soldeannuel)  && (c.type == "Annuel"))
                        {


                        service.CreateConge(c);

                    }


                    if (((c.Duree + maladie+validmal) > Soldemaladie) && (c.type == "Maladie"))
                    {
                        TempData["msg"] = "Vous avez depassé ton solde du congé de maladie";

                        // TempData["msg"] = "<script>alert('Vous avez depassé ton solde du congé de maladie');</script>";

                        // c.Maladie = maladie;


                        return View();


                    }
                    else if (((c.Duree + maladie+validmal) <= Soldemaladie) && (c.type == "Maladie"))
                    {

                        service.CreateConge(c);

                    }


                    if (((c.Duree + maternite+validmat) > Soldematernite) && (c.type == "Maternité"))
                    {


                        // TempData["msg"] = "<script>alert('Vous avez depassé ton solde du congé de maternité');</script>";

                        TempData["msg"] = "Vous avez depassé ton solde du congé de maternité";

                        return View();


                    }
                    else if (((c.Duree + maternite) <= Soldematernite) && (c.type == "Maternité"))
                    {


                        service.CreateConge(c);

                    }


                }
                else if ((con != null) && (sol != null))
                {
                    
                    double annuel = service.GetSoldeByUser(s.Userid).LastOrDefault().Annuel;
                    double maladie = service.GetSoldeByUser(s.Userid).LastOrDefault().Maladie;
                    double maternite = service.GetSoldeByUser(s.Userid).LastOrDefault().Maternité;
                    double validmal = service.GetCongeByUser(c.Userid).Where(x => x.Statut == "à valider").Where(x => x.type == "Maladie").Sum(x => x.Duree);
                    double validmat = service.GetCongeByUser(c.Userid).Where(x => x.Statut != "à valider").Where(x => x.type == "Maternité").Sum(x => x.Duree);
                    double valideann = service.GetCongeByUser(c.Userid).Where(x => x.Statut == "à valider").Where(x => x.type == "Annuel").Sum(x => x.Duree);
                    if (c.type == "Sans solde")
                    { service.CreateConge(c); }
                    if (((c.Duree + annuel + valideann) > Soldeannuel) && (c.type == "Annuel"))
                    {

                        TempData["msg"] = "Vous avez depassé ton solde du congé annuel";
                        //TempData["msg"] = "<script>alert('Vous avez depassé ton solde du congé annuel');</script>";
                        //c.Annuel = annuel;


                        return View();


                    }



                    else if (((c.Duree + annuel + valideann) <= Soldeannuel) && (c.type == "Annuel"))
                    {


                        service.CreateConge(c);

                    }


                    if (((c.Duree + maladie + validmal) > Soldemaladie) && (c.type == "Maladie"))
                    {


                        // TempData["msg"] = "<script>alert('Vous avez depassé ton solde du congé de maladie');</script>";

                        TempData["msg"] = "Vous avez depassé ton solde du congé de maladie";


                        return View();


                    }
                    else if (((c.Duree + maladie + validmal) <= Soldemaladie) && (c.type == "Maladie"))
                    {

                        service.CreateConge(c);

                    }


                    if (((c.Duree + maternite + validmat) > Soldematernite) && (c.type == "Maternité"))
                    {


                        //  TempData["msg"] = "<script>alert('Vous avez depassé ton solde du congé de maternité');</script>";

                        TempData["msg"] = "Vous avez depassé ton solde du congé de maternité";

                        return View();


                    }
                    else if (((c.Duree + maternite + validmat) <= Soldematernite) && (c.type == "Maternité"))
                    {


                        service.CreateConge(c);

                    }


                }




                return RedirectToAction("Index");
               
            }
                return View(c);
       }

        //***************************Index***************************//
          [Authorize]
          public IActionResult Index(string search)
          {if (search == null)
              {string user= User.Identity.Name;
                  var Conges = service.GetCongeByUser(user);

                return View(Conges);
              }
          else
              {
                 var Conges =  service.GetCongeBytype(search);
                  return View(Conges);
              }

          }

        ///************************Edit*************************//
         [Authorize]
         public IActionResult Edit(int id)
         {
             Conge c = service.GetConge(id);
             return View(c);

         }


         [HttpPost]

         public IActionResult Edit(Conge c, Solde s)
         {

           
            double validmal = service.GetCongeByUser(c.Userid).Where(x => x.Statut == "à valider").Where(x => x.type == "Maladie").Sum(x => x.Duree);
             double validmat = service.GetCongeByUser(c.Userid).Where(x => x.Statut == "à valider").Where(x => x.type == "Maternité").Sum(x => x.Duree);
             double valideann = service.GetCongeByUser(c.Userid).Where(x => x.Statut == "à valider").Where(x => x.type == "Annuel").Sum(x => x.Duree);

              if(validmal==0)

             {  validmal = validmal-0; }
             else if (validmal != 0)
             { validmal = validmal-c.Duree; }
             if (validmat == 0)
             {  validmat = validmat-0; }
             else if (validmat != 0)
             {  validmat = validmat - c.Duree; }

             if (valideann == 0)
             { valideann = valideann-0; }
             else if (valideann != 0)
             {  valideann = valideann - c.Duree; }

              var sol = service.GetSoldeByUser(s.Userid).LastOrDefault();


             if (ModelState.IsValid)
             {
                 //c.Statut = statut;
                 c.Userid = User.Identity.Name;
                 s.Userid = User.Identity.Name;

                 double t = (c.DateFin - c.DateDeb).Value.TotalDays;
                 c.Duree = t;
                if ((c.type == "Sans solde") && (c.Statut == "à valider"))
                {


                    service.UpdateConge(c);

                }
                else if ((c.type == "Sans solde") && (c.Statut != "à valider"))
                {
                    TempData["msg"] = "Cette demande est déjà traitée";                   
                    return View();
                }
                

                if (sol != null)
                 {
                     double annuel = service.GetSoldeByUser(s.Userid).LastOrDefault().Annuel;
                     double maladie = service.GetSoldeByUser(s.Userid).LastOrDefault().Maladie;
                     double maternite = service.GetSoldeByUser(s.Userid).LastOrDefault().Maternité;


                     if (((c.Duree + annuel + valideann) > Soldeannuel) && (c.type == "Annuel") && (c.Statut == "à valider"))
                     {
                        TempData["msg"] = "Vous avez depassé ton solde du congé annuel";
                        
                         return View();
                     }



                     else if (((c.Duree + annuel + valideann) <= Soldeannuel) && (c.type == "Annuel") && (c.Statut == "à valider"))
                     {
                        service.UpdateConge(c);
                     }


                     if (((c.Duree + maladie + validmal) > Soldemaladie) && (c.type == "Maladie") && (c.Statut == "à valider"))
                     {
                         TempData["msg"] = "Vous avez depassé ton solde du congé de maladie";
                       
                         return View();
                     }
                     else if (((c.Duree + maladie + validmal) <= Soldemaladie) && (c.type == "Maladie") && (c.Statut == "à valider"))
                     {

                         service.UpdateConge(c);

                     }


                     if (((c.Duree + maternite + validmat) > Soldematernite) && (c.type == "Maternité") && (c.Statut == "à valider"))
                     {

                         TempData["msg"] = "Vous avez depassé ton solde du congé de maternité";
                        
                         return View();
                     }
                     else if (((c.Duree + maternite + validmat) <= Soldematernite) && (c.type == "Maternité") && (c.Statut == "à valider"))
                     {


                         service.UpdateConge(c);

                     }
                 }


                 if (sol == null)
                 {
                     double annuel = 0;
                     double maladie = 0;
                     double maternite = 0;
                  
                     if (((c.Duree + annuel + valideann) > Soldeannuel) && (c.type == "Annuel") && (c.Statut == "à valider"))
                     {

                         TempData["msg"] = "Vous avez depassé ton solde du congé annuel";                       
                         return View();

                     }



                     else if (((c.Duree + annuel + valideann) <= Soldeannuel) && (c.type == "Annuel") && (c.Statut == "à valider"))
                     {

                         service.UpdateConge(c);
                     }


                     if (((c.Duree + maladie + validmal) > Soldemaladie) && (c.type == "Maladie") && (c.Statut == "à valider"))
                     {
                      
                         TempData["msg"] = "Vous avez depassé ton solde du congé de maladie";

                         return View();

                     }
                     else if (((c.Duree + maladie + validmal) <= Soldemaladie) && (c.type == "Maladie") && (c.Statut == "à valider"))
                     {

                         service.UpdateConge(c);

                     }


                     if (((c.Duree + maternite + validmat) > Soldematernite) && (c.type == "Maternité") && (c.Statut == "à valider"))
                     {
                
                         TempData["msg"] = "Vous avez depassé ton solde du congé de maternité";

                         return View();
                     }
                     else if (((c.Duree + maternite + validmat) <= Soldematernite) && (c.type == "Maternité") && (c.Statut == "à valider"))
                     {

                         service.UpdateConge(c);

                     }
                 }



               

                 return RedirectToAction("Index");
             }

             return View(c);
         }
    
        /////*********************Delete************************//

        [Authorize]
        public IActionResult Delete(int id)
        {
           
            Conge con = service.GetConge(id);
            return View(con);
        }
        [HttpPost, ActionName("Delete")]
       
        public IActionResult DeleteCong(int id)

        {
          
            try
            {
                if (ModelState.IsValid)
                { 
                    Conge conn = service.GetConge(id);
                  
                    
                        service.DeleteConge(conn);
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

            Conge conn = service.GetConge(id);

            return View(conn);

        }

        //***********************Solde*******************************//

        [Authorize]
        public IActionResult Solde(Solde s)
        {
           
            s.Userid = User.Identity.Name;
        


            Solde solde =service.GetSoldeByUser(s.Userid).LastOrDefault();
            return View(solde);

        }

    }

}























