using System;
using System.Collections.Generic;

namespace SIRHCoreData.Models.DB
{
    public partial class TblDimFonction
    {
        public TblDimFonction()
        {
            TblDwSalarie = new HashSet<TblDwSalarie>();
        }

        public int IdFonction { get; set; }
        public int IdDirection { get; set; }
        public string LibelleFonction { get; set; }

        public virtual TblDimDirection IdDirectionNavigation { get; set; }
        public virtual ICollection<TblDwSalarie> TblDwSalarie { get; set; }
    }
}
