using System;
using System.Collections.Generic;

namespace SIRHCoreData.Models.DB
{
    public partial class TblDwSalarie
    {
        public int Matricule { get; set; }
        public string Civilite { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public int IdFonction { get; set; }
        public int IdStructure { get; set; }
        public string Tel { get; set; }

        public virtual TblDimFonction IdFonctionNavigation { get; set; }
        public virtual TblDimStructure IdStructureNavigation { get; set; }
        public virtual TblDwEtatCivil TblDwEtatCivil { get; set; }
    }
}
