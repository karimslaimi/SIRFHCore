using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIRHCoreDomain
{
    public class Solde
    {
        [Key]
        public int SoldeID { get; set; }
       public double Annuel { get; set; }
        public double Maternité { get; set; }
        public double Payé { get; set; }
        public double SansSolde { get; set; }
        public double Maladie { get; set; }
        public string Userid { get; set; }
        public virtual ICollection<Personne> Persoone { get; set; }
        // public virtual ICollection<Conge> Conge { get; set; }

        // public virtual Conge Conge { get; set; }
        //public virtual Personne Personne { get; set; }
    }
}
