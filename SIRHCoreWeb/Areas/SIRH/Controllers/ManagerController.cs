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
        }
        ICongeService service = null;
        IFraisService servicef = null;
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