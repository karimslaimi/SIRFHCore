using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRHCoreDomain
{
    public class Document
    {

        public int Id { get; set; }
        public string type { get; set; }
        public DateTime? DateAjout { get; set; }
        public string Propritere { get; set; }

    }
}
