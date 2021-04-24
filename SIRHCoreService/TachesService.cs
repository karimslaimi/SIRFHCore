using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace SIRHCoreService
{
    public class TachesService : ITachesService
    {

        static IDatabaseFactory dbf = null;
        static IUnitOfWork uow = null;
        public TachesService()
        {
            dbf = new DatabaseFactory();
            uow = new UnitOfWork(dbf);

        }


        public Taches GetTaches(int id)
        {
            return dbf.DataContext.Taches.Include(x => x.creator).Include(x=>x.Projet).Where(x => x.id == id).FirstOrDefault();
        }


        public void Add(Taches entity)
        {
            uow.TachesRepository.Add(entity);
            uow.Commit();
        }


        public void Delete(Taches entity)
        {
            uow.TachesRepository.Delete(entity);
            uow.Commit();
        }

        public void Delete(Expression<Func<Taches, bool>> where)
        {
            uow.TachesRepository.Delete(where);
            uow.Commit();
        }

        public Taches Get(Expression<Func<Taches, bool>> where)
        {
            return uow.TachesRepository.Get(where);
        }



        public IEnumerable<Taches> GetAll()
        {
            return uow.TachesRepository.GetAll();
        }

        public IEnumerable<Taches> GetAlll(string user)
        {
            throw new NotImplementedException();
        }

        public Taches GetById(long Id)
        {
            return uow.TachesRepository.GetById(Id);
        }

        public Taches GetById(string Id)
        {
            return uow.TachesRepository.GetById(Id);
        }

        public IEnumerable<Taches> GetMany(Expression<Func<Taches, bool>> where)
        {
            return uow.TachesRepository.GetMany(where);
        }

        public Taches Getsolde(Expression<Func<Taches, bool>> where)
        {
            throw new NotImplementedException();
        }

        public List<Taches> GetTachesbyProjct(int id)
        {
            return dbf.DataContext.Taches.Include(s => s.creator).Where(x => x.Projet.id == id).ToList();
        }

        public void Update(Taches entity)
        {
            uow.TachesRepository.Update(entity);
            uow.Commit();
        }

        public void Updatee(Taches entity)
        {
            uow.TachesRepository.Update(entity);
            uow.Commit();
        }
    }
}
