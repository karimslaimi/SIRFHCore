using System;
using System.Collections.Generic;

namespace SIRHCoreData.Models.DB
{
    public partial class TblDimSituationFamiliale
    {
        public TblDimSituationFamiliale()
        {
            TblDwEtatCivil = new HashSet<TblDwEtatCivil>();
        }

        public int IdSituationFamiliale { get; set; }
        public string LibelleSituationFamiliale { get; set; }

        public virtual ICollection<TblDwEtatCivil> TblDwEtatCivil { get; set; }
    }
}
