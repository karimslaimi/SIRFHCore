using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRHCoreDomain
{
    public class Projet
    {
        [Key]
        public int id { get; set; }
        public string nom { get; set; }
        public DateTime datedeb { get; set; }
        public DateTime datefin { get; set; }
        public string description { get; set; }

        public string createurId { get; set; }
        public virtual Personne createur { get; set; }

        public virtual List<Collaboration> collaborateurs { get; set; }
        public virtual List<Taches> Taches { get; set; }



    }
}
