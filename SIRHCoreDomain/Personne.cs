//using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
////using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SIRHCoreDomain
{
    public class Personne : IdentityUser
    {
        public Personne()
        {
            Conge = new List<Conge>();
            NoteDeFrais = new List<NoteDeFrais>();
        }


        [Key]
        public int PersonneID { get;  }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int CIN { get; set; }
        public string Adresse { get; set; }
        public string DateDeNais { get; set; }
        public string Grade { get; set; }
        public string Fonction { get; set; }
        public virtual Solde Solde { get; set; }
        public virtual List<Conge> Conge { get; set; }
        public virtual List<Projet> Projets { get; set; }
        public virtual List<Collaboration> Collaborations { get; set; }
        public virtual List<Taches> Taches { get; set; }
       // public virtual ICollection<Solde> Solde { get; set; }
        public virtual List<NoteDeFrais> NoteDeFrais { get; set; }
       



       // public virtual  IdentityUser AspNetUsers { get; set; }
       ///public virtual IdentityUser IdentityUser { get; set; }
    }
    /* public class ApplicationUser : IdentityUser
     {
         //public virtual Personne Personne { get; set; }
     }*/
    //public InputModel Input { get; set; }
      
}

