using System;
using System.Collections.Generic;

namespace SIRHCoreData.Models.DB
{
    public partial class TblDimVille
    {
        public TblDimVille()
        {
            TblDwEtatCivil = new HashSet<TblDwEtatCivil>();
        }

        public int IdVille { get; set; }
        public int IdPays { get; set; }
        public string NomVille { get; set; }

        public virtual TblDimPays IdPaysNavigation { get; set; }
        public virtual ICollection<TblDwEtatCivil> TblDwEtatCivil { get; set; }
    }
}
