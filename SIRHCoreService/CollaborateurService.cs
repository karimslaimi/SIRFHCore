using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIRHCoreService
{
    public class CollaborateurService : ICollaborateurService
    {
        static IDatabaseFactory dbf = null;
        static IUnitOfWork uow = null;
        public CollaborateurService()
        {
            dbf = new DatabaseFactory();
            uow = new UnitOfWork(dbf);

        }


        public void Add(Collaboration entity)
        {
            uow.CollaborateurRepository.Add(entity);
            uow.Commit();
        }


        public void Delete(Collaboration entity)
        {
            uow.CollaborateurRepository.Delete(entity);
            uow.Commit();
        }

        public void Delete(Expression<Func<Collaboration, bool>> where)
        {
            uow.CollaborateurRepository.Delete(where);
            uow.Commit();
        }

        public Collaboration Get(Expression<Func<Collaboration, bool>> where)
        {
            return uow.CollaborateurRepository.Get(where);
        }



        public IEnumerable<Collaboration> GetAll()
        {
            return uow.CollaborateurRepository.GetAll();
        }

        public IEnumerable<Collaboration> GetAlll(string user)
        {
            throw new NotImplementedException();
        }

        public Collaboration GetById(long Id)
        {
            return uow.CollaborateurRepository.GetById(Id);
        }

        public Collaboration GetById(string Id)
        {
            return uow.CollaborateurRepository.GetById(Id);
        }

        public IEnumerable<Collaboration> GetMany(Expression<Func<Collaboration, bool>> where)
        {
            return uow.CollaborateurRepository.GetMany(where);
        }

        public Collaboration Getsolde(Expression<Func<Collaboration, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Update(Collaboration entity)
        {
            uow.CollaborateurRepository.Update(entity);
            uow.Commit();
        }

        public void Updatee(Collaboration entity)
        {
            uow.CollaborateurRepository.Update(entity);
            uow.Commit();
        }
    }
}
