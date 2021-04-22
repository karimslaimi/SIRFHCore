using System;

using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SIRHCoreDomain;
namespace SIRHCoreService
{
    public interface ICongeService : IDisposable
    {

        void CreateConge(Conge c);
        void solde(Solde s);
        void add(Conge c);
        Conge GetConge(int CongeID);
        Conge GetConge(string Userid);
        
        //Conge GetConge(Conge C);
        IEnumerable<Conge> GetConge();

        void UpdateConge(Conge c);

        void DeleteConge(Conge c);
        void traiterConge([Bind("Statut")] Conge c);
        IEnumerable<Conge> GetCongeBytype(string search);
        IEnumerable<Conge> GetCongeByUser(string user);
        IEnumerable<Conge> GetCongeByStatut(string search);

        // Conge GetCongeByIdentity(string User);
        IEnumerable<Conge> GetCongeThisUser(string user);
        IEnumerable<Conge> GetCongeB(string search);
        IEnumerable<Solde> GetSoldeB(string search);
        IEnumerable<Solde> GetSoldeByUser(string user);
    }
}
