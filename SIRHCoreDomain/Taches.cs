using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRHCoreDomain
{
    public class Taches
    {
        [Key]
        public int id { get; set; }

        public string title { get; set; }

        public string description { get; set; }
        public DateTime date { get; set; }
 
        public virtual Personne creator { get; set; }
        public virtual Projet Projet { get; set; }
        public string state { get; set; }
    
    }
}
