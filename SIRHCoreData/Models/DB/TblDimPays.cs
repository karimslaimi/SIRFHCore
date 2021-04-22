using System;
using System.Collections.Generic;

namespace SIRHCoreData.Models.DB
{
    public partial class TblDimPays
    {
        public TblDimPays()
        {
            TblDimVille = new HashSet<TblDimVille>();
        }

        public int IdPays { get; set; }
        public string NomPays { get; set; }

        public virtual ICollection<TblDimVille> TblDimVille { get; set; }
    }
}
