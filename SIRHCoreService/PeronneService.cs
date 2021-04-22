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
    public class PersonneService :IPersonneService
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public PersonneService()
        {
            dbf = new DatabaseFactory();
            uow = new UnitOfWork(dbf);
        }

        public void Add(Personne entity)
        {
            uow.PersonneRepository.Add(entity);
            uow.Commit();
        }


        public void Delete(Personne entity)
        {
            uow.PersonneRepository.Delete(entity);
            uow.Commit();
        }

        public void Delete(Expression<Func<Personne, bool>> where)
        {
            uow.PersonneRepository.Delete(where);
            uow.Commit();
        }

        public Personne Get(Expression<Func<Personne, bool>> where)
        {
            return uow.PersonneRepository.Get(where);
        }



        public IEnumerable<Personne> GetAll()
        {
            return uow.PersonneRepository.GetAll();
        }

        public IEnumerable<Personne> GetAlll(string user)
        {
            throw new NotImplementedException();
        }

        public Personne GetById(long Id)
        {
            return uow.PersonneRepository.GetById(Id);
        }

        public Personne GetById(string Id)
        {
            return uow.PersonneRepository.GetById(Id);
        }

        public IEnumerable<Personne> GetMany(Expression<Func<Personne, bool>> where)
        {
            return uow.PersonneRepository.GetMany(where);
        }

        public Personne Getsolde(Expression<Func<Personne, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Update(Personne entity)
        {
            uow.PersonneRepository.Update(entity);
            uow.Commit();
        }

        public void Updatee(Personne entity)
        {
            uow.PersonneRepository.Update(entity);
            uow.Commit();
        }
    }
}

