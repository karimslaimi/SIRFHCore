using System;
using System.Collections.Generic;

namespace SIRHCoreData.Models.DB
{
    public partial class TblDimDirection
    {
        public TblDimDirection()
        {
            TblDimDepartement = new HashSet<TblDimDepartement>();
            TblDimFonction = new HashSet<TblDimFonction>();
        }

        public int IdDirection { get; set; }
        public string LibelleDirection { get; set; }

        public virtual ICollection<TblDimDepartement> TblDimDepartement { get; set; }
        public virtual ICollection<TblDimFonction> TblDimFonction { get; set; }
    }
}
