using System;
using System.Collections.Generic;

namespace SIRHCoreData.Models.DB
{
    public partial class TblDimDepartement
    {
        public int IdDepartement { get; set; }
        public int IdDirection { get; set; }
        public string LibelleDepartement { get; set; }

        public virtual TblDimDirection IdDirectionNavigation { get; set; }
    }
}
