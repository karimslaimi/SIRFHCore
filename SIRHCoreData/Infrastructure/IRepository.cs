using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SIRHCoreData.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Updatee(T entity);
        void Delete(T entity);
        //void TelechargerDocument(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long Id);
        T GetById(string Id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAlll(string user);
       // IEnumerable<T> GetByPropritere(string Propritere);
       // IEnumerable<T> GetByType(string type);
       // IEnumerable<T> GetByDateCreation(DateTime datecreation);
       // IEnumerable<T> GetByDateReglage(DateTime datereglage);
       // IEnumerable<T> GetBycreepar(string creepar);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        T Getsolde(Expression<Func<T, bool>> where);
       
    }

}