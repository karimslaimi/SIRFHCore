using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIRHCoreDomain;
using SIRHCoreData.Repositories;


namespace SIRHCoreData.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private SIRHcontext dataContext;
        IDatabaseFactory dbFactory;
        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        private ITachesRepository tachesRepository;
        public ITachesRepository TachesRepository
   
        {
            get { return tachesRepository = new TachesRepository(dbFactory); }
        }





        private ICollaborateurRepository collaborateurRepository;
        public ICollaborateurRepository CollaborateurRepository
    
        {
            get { return collaborateurRepository = new CollaborateurRepository(dbFactory); }
        }




        private IProjetRepository projetRepository;
        public IProjetRepository ProjetRepository
        //IPersonneRepository IUnitOfWork.PersonneRepository
        {
            get { return projetRepository = new ProjetRepository(dbFactory); }
        }





        private IPersonneRepository personneRepository;
        public IPersonneRepository PersonneRepository
        //IPersonneRepository IUnitOfWork.PersonneRepository
        {
            get { return personneRepository = new PersonneRepository(dbFactory); }
        }







        private ICongeRepository congeRepository;
        public ICongeRepository CongeRepository
       // ICongeRepository IUnitOfWork.CongeRepository
        {
            get { return congeRepository = new CongeRepository(dbFactory); }
        }
        private INoteDeFraisRepository notedefraisRepository;
        public INoteDeFraisRepository NoteDeFraisRepository
        //INoteDeFraisRepository IUnitOfWork.NoteDeFraisRepository
        {
            get { return notedefraisRepository = new NoteDeFraisRepository(dbFactory); }
        }



        private ISoldeRepository soldeRepository;
        public ISoldeRepository SoldeRepository
       // ISoldeRepository IUnitOfWork.SoldeRepository
        {
            get { return soldeRepository = new SoldeRepository(dbFactory); }
        }

        private IIncidentRepository incidentRepository;
        public IIncidentRepository IncidentRepository
        // IIncidentRepository IUnitOfWork.IncidentRepository
        {
            get { return incidentRepository = new IncidentRepository(dbFactory); }
        }
        private IDocumentRepository documentRepository;
        public IDocumentRepository  DocumentRepository
        // IDocumentRepository IUnitOfWork.DcumentRepository
        {
            get { return documentRepository = new DocumentRepository(dbFactory); }
        }
        protected SIRHcontext DataContext
        {
            get
            {
                return dataContext = dbFactory.DataContext;
            }
        }
        public void Commit()
        {
            DataContext.SaveChanges();
        }
        public void Dispose()
        {
            dbFactory.Dispose();
        }
    }

}