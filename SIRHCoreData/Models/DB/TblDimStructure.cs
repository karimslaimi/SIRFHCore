using System;
using System.Collections.Generic;

namespace SIRHCoreData.Models.DB
{
    public partial class TblDimStructure
    {
        public TblDimStructure()
        {
            TblDwSalarie = new HashSet<TblDwSalarie>();
        }

        public int IdStructure { get; set; }
        public string LibelleStructure { get; set; }

        public virtual ICollection<TblDwSalarie> TblDwSalarie { get; set; }
    }
}
