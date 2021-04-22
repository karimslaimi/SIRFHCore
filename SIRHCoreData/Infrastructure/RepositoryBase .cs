using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SIRHCoreData.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository <T> where T : class
    {
        private SIRHcontext dataContext;
        private DbSet<T> dbset;
        IDatabaseFactory databaseFactory;
        protected RepositoryBase(IDatabaseFactory dbFactory)
        {
            this.databaseFactory = dbFactory;
            dbset = DataContext.Set<T>();
        }
        protected SIRHcontext DataContext
        {
            get { return dataContext = databaseFactory.DataContext; }
        }
        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }
        
        public virtual void Update(T entity)
        {

            dbset.Attach(entity);
      
            dataContext.Entry(entity).State =  EntityState.Modified;
        
        }
       public virtual void Updatee(T entity)
        {

            dbset.Attach(entity);
           
            dataContext.Entry(entity).State = EntityState.Added;
            
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }
        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }
        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }
        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }
        public virtual IEnumerable<T> GetAlll(string user)
        {
            return dbset.ToList();
        }


        public T Getsolde(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).Last<T>();
        }


       
    }

}
