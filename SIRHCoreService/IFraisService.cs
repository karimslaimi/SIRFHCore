using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SIRHCoreDomain;


namespace SIRHCoreService
{
    public interface IFraisService : IDisposable
    {
        NoteDeFrais GetFrais(string Userid);
        void CreateFrais(NoteDeFrais f);
    
        NoteDeFrais GetFrais(int NoteDeFraisID);
        

        //Conge GetConge(Conge C);
        IEnumerable<NoteDeFrais> GetFrais();

        void UpdateFrais(NoteDeFrais F);
      
        void DeleteFrais(NoteDeFrais F);
        void traiterFrais([Bind("Statut")] NoteDeFrais f);
        IEnumerable<NoteDeFrais> GetFraisByStatut(string search);
        IEnumerable<NoteDeFrais> GetFraisByUser(string user);

        // Conge GetCongeByIdentity(string User);
      //  IEnumerable<NoteDeFrais GetFraisThisUser(string user);
       IEnumerable<NoteDeFrais> GetFraisB(string search);
    }
}
