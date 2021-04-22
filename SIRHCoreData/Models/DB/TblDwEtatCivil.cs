using System;
using System.Collections.Generic;

namespace SIRHCoreData.Models.DB
{
    public partial class TblDwEtatCivil
    {
        public int Matricule { get; set; }
        public string NumSecuSociale { get; set; }
        public DateTime? DateNaissance { get; set; }
        public int IdPaysNaissance { get; set; }
        public int IdVilleNaissance { get; set; }
        public string Nationalite { get; set; }
        public int IdSituationFamiliale { get; set; }
        public string Adresse { get; set; }
        public string EmailPersonnel { get; set; }
        public string Linkedin { get; set; }

        public virtual TblDimSituationFamiliale IdSituationFamilialeNavigation { get; set; }
        public virtual TblDimVille IdVilleNaissanceNavigation { get; set; }
        public virtual TblDwSalarie MatriculeNavigation { get; set; }
    }
}
