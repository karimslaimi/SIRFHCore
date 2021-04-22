using System;
using System.Collections.Generic;
using System.Text;
using SIRHCoreData.Repositories;
namespace SIRHCoreData.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
       IPersonneRepository PersonneRepository { get; }
       ISoldeRepository SoldeRepository { get; }
       ICongeRepository CongeRepository { get; }
      INoteDeFraisRepository NoteDeFraisRepository { get; }
        IIncidentRepository IncidentRepository { get; }
        IDocumentRepository DocumentRepository { get; }

        IProjetRepository ProjetRepository { get; }
         ICollaborateurRepository CollaborateurRepository { get; }
    //  void Disposable();
    }

}
