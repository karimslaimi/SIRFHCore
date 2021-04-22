using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SIRHCoreDomain
{
    public class Conge
    {
        [Key]

        public int CongeID { get; set; }
        public DateTime? DateDeb { get; set; }
        public DateTime? DateFin { get; set; }
        public string type { get; set; }
        public string Justificatif { get; set; }
        public string CommentaireC { get; set; }
        public string Userid { get; set; }
        public string Statut { get; set; }
        public virtual Personne Personne { get; set; }
        //public virtual Solde Solde { get; set; }

        public double Annuel { get; set; }
        public double Maternite { get; set; }
        public double SansSolde { get; set; }
        public double Maladie { get; set; }
        public string RH { get; set; }
        public string CommentaireRH { get; set; }
       public double Duree { get; set; }
        [NotMapped]

        public IEnumerable<SelectListItem> TypeList
        {
            get
            {
                return new List<SelectListItem>
        {
            new SelectListItem { Text = "Annuel", Value = "Annuel"},
            new SelectListItem { Text = "Maternité", Value = "Maternité"},
          
            new SelectListItem { Text = "Maladie", Value = "Maladie"},
            new SelectListItem { Text = "Sans solde", Value = "Sans solde"}
        };
            }
            


        }
        [NotMapped]

        public IEnumerable<SelectListItem> StatutList
        {
            get
            {
                return new List<SelectListItem>
        {
            new SelectListItem { Text = "Accepter", Value = "Accepter"},
            new SelectListItem { Text = "Refuser", Value = "Refuser"},
             new SelectListItem { Text = "à valider", Value = "Refuser"}

        };
            }
        }
    }
}
