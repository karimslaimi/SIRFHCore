using SIRHCoreData.Infrastructure;
using SIRHCoreData.Repositories;
using SIRHCoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SIRHCoreService
{
    public class ProjetService:IProjetService
    {
        static IDatabaseFactory dbf = null;
        static IUnitOfWork uow = null;
        public ProjetService() 
        {
            dbf = new DatabaseFactory();
            uow = new UnitOfWork(dbf);

        }

        public void Add(Projet entity)
        {
            uow.ProjetRepository.Add(entity);
            uow.Commit();
        }
 

        public void Delete(Projet entity)
        {
            uow.ProjetRepository.Delete(entity);
            uow.Commit();
        }

        public void Delete(Expression<Func<Projet, bool>> where)
        {
            uow.ProjetRepository.Delete(where);
            uow.Commit();
        }

        public Projet Get(Expression<Func<Projet, bool>> where)
        {
            return uow.ProjetRepository.Get(where);
        }

       

        public IEnumerable<Projet> GetAll()
        {
            return uow.ProjetRepository.GetAll();
        }

        public IEnumerable<Projet> GetAlll(string user)
        {
            throw new NotImplementedException();
        }

        public Projet GetById(long Id)
        {
            return uow.ProjetRepository.GetById(Id);
        }

        public Projet GetById(string Id)
        {
            return uow.ProjetRepository.GetById(Id);
        }

        public IEnumerable<Projet> GetMany(Expression<Func<Projet, bool>> where)
        {
            return uow.ProjetRepository.GetMany(where);
        }

        public Projet GetProjet(int id)
        {
            return dbf.DataContext.Projets.Where(x=>x.id==id).Include(x => x.createur).Include(s=>s.collaborateurs).Include(s=>s.Taches).FirstOrDefault();
        }

        public Projet Getsolde(Expression<Func<Projet, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Update(Projet entity)
        {
            uow.ProjetRepository.Update(entity);
            uow.Commit();
        }

        public void Updatee(Projet entity)
        {
            uow.ProjetRepository.Update(entity);
            uow.Commit();
        }
    }
}
