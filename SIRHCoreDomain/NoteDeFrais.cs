using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SIRHCoreDomain
{
    public class NoteDeFrais
    {
        [Key]
        public int NoteDeFraisID { get; set; }
        public string Type { get; set; }
        public double Montant { get; set; }
        public string Justificatif { get; set; }
        public string CommentaireF { get; set; }
        public string Userid { get; set; }
        public DateTime? Date { get; set; }
        public int nbrper { get; set; }
        public string Statut { get; set; }
        public string RH { get; set; }
        public string CommentaireRH { get; set; }
        public virtual Personne Personne { get; set; }
        [NotMapped]

        public IEnumerable<SelectListItem> TypeList
        {
            get
            {
                return new List<SelectListItem>
        {
            new SelectListItem { Text = "Frais de séjours", Value = "Frais de séjours"},
            new SelectListItem { Text = "Frais de déplacements", Value = "Frais de déplacements"},

            
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
