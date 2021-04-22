using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRHCoreDomain
{
    public class Collaboration
    {
        [Key]
        public int id { get; set; }

        public virtual Personne Personne{get;set;}
        public virtual Projet Projet { get; set; }
    
    
    }
}
