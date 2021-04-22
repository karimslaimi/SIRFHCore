using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRHCoreDomain
{
    public class Incident
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreation { get; set; }
        public DateTime? DateReglage { get; set; }
        public string Creepar { get; set; }
        public string Modifiepar { get; set; }
        public virtual Personne Attribution { get; set; }
    }
}
