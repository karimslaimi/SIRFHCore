using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;

namespace SIRHCoreService
{
    public class IncidentService : IIncidentService
    {

        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        
        public IncidentService()
        {
            dbFactory = new DatabaseFactory();
            utOfWork = new UnitOfWork(dbFactory);
        }
        public IEnumerable<Incident> GetIncident()
        {
            var Incident = utOfWork.IncidentRepository.GetAll();
            return Incident;
        }


        public void createIncident(Incident i)
        {
            utOfWork.IncidentRepository.Add(i);
            utOfWork.Commit();
        }
        public void Add(Incident i)
        {
            utOfWork.IncidentRepository.Add(i);
        }
        public void UpdateIncident(Incident i)
        {
            if (i.Id > 0)
            {
                utOfWork.IncidentRepository.Update(i);
                utOfWork.Commit();
            }
            }
        public void DeleteIncident(Incident i)
        {
            utOfWork.IncidentRepository.Delete(i);
            utOfWork.Commit();
        }
        public void traiterIncident([Bind ("Id")]Incident i)
        {
            utOfWork.IncidentRepository.Update(i);
            utOfWork.Commit();
        }
        //public void AttribuerIncident(Incident i)
        //{
        //    utOfWork.IncidentRepository.Update(i);
        //    utOfWork.Commit();
        //}
        public Incident GetIncident(int Id)
        { var Incident = utOfWork.IncidentRepository.GetById(Id);
                return Incident;
        }
        //public Incident GetIncidentByDateCreation (DateTime datecreation)
        //{ var Incident = utOfWork.IncidentRepository.GetByDateCreation(datecreation); 
        //    return (Incident)Incident;
        //}
        //public Incident GetIncidentByDateReglage(DateTime datereglage)
        //{
        //    var Incident = utOfWork.IncidentRepository.GetByDateReglage (datereglage);
        //    return (Incident)Incident;
        //}
        //public Incident GetIncidentBycreepar(string creepar)
        //{
        //    var Incident=utOfWork .IncidentRepository.GetBycreepar (creepar);
        //    return(Incident) Incident;
        //}
        public void Dispose()
        {
            utOfWork.Dispose();
        }

        public void Update(Incident entity)
        {
            utOfWork.IncidentRepository.Update(entity);
            utOfWork.Commit();
        }

        public void Updatee(Incident entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Incident entity)
        {
            utOfWork.IncidentRepository.Delete(entity);
            utOfWork.Commit();
        }

        public void Delete(Expression<Func<Incident, bool>> where)
        {
            utOfWork.IncidentRepository.Delete(where);
            utOfWork.Commit();
        }

        public Incident GetById(long Id)
        {
           return utOfWork.IncidentRepository.Get(w => w.Id == Id);
        }

        public Incident GetById(string Id)
        {
            return utOfWork.IncidentRepository.Get(w => w.Id.ToString()== Id);
        }

        public Incident Get(Expression<Func<Incident, bool>> where)
        {
            return utOfWork.IncidentRepository.Get(where);
        }

        public IEnumerable<Incident> GetAll()
        {
            return utOfWork.IncidentRepository.GetAll();
        }

        public IEnumerable<Incident> GetAlll(string user)
        {
            return utOfWork.IncidentRepository.GetMany(x=>x.Creepar.UserName==user);
        }

        public IEnumerable<Incident> GetMany(Expression<Func<Incident, bool>> where)
        {
            return utOfWork.IncidentRepository.GetMany(where);
        }

        public Incident Getsolde(Expression<Func<Incident, bool>> where)
        {
            throw new NotImplementedException();
        }
    }
}
