using System;
using System.Collections.Generic;
using System.Text;
///sing System.Web.Mvc;
using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
///using System.Threading.Tasks;

namespace SIRHCoreService
{
    public class CongeService : ICongeService

    {


        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        public CongeService()
        {
            dbFactory = new DatabaseFactory();
            utOfWork = new UnitOfWork(dbFactory);
        }


        public IEnumerable<Conge> GetConge()
        {
            var Conges = utOfWork.CongeRepository.GetAll();
            return Conges;
        }
        public void CreateConge(Conge c)
        //[Bind(Include ="DateDeb, DateFin, type, CommentaireC")]

        {
          // c.Userid =User.Identity.Name;

            utOfWork.CongeRepository.Add(c);
            utOfWork.Commit();
        }
        public void add(Conge c)
        //[Bind(Include ="DateDeb, DateFin, type, CommentaireC")]

        {
            // c.Userid =User.Identity.Name;

            utOfWork.CongeRepository.Add(c);
         
        }
        
       public void UpdateConge(Conge c)
        {if (c.CongeID > 0)
            {
                utOfWork.CongeRepository.Update(c);
                utOfWork.Commit();
            }
            else {
                utOfWork.CongeRepository.Update(c);
                utOfWork.Commit();
            }
        }

        public void traiterConge([Bind("Statut")] Conge c)
        {
            utOfWork.CongeRepository.Update(c);
            utOfWork.Commit();
        }
        public void DeleteConge(Conge c)
        {
            utOfWork.CongeRepository.Delete(c);
            utOfWork.Commit();
        }
        public Conge GetConge(int CongeID)
        {
            var Conge = utOfWork.CongeRepository.GetById(CongeID);
            return Conge;
        }
        public Conge GetConge(string Userid)
        {
            var Conge = utOfWork.CongeRepository.GetById(Userid);
            return Conge;
        }
        public IEnumerable<Conge> GetCongeByUser(string user)
        {
            return utOfWork.CongeRepository.GetMany(x => x.Userid==user);
        }
        public IEnumerable<Conge> GetCongeBytype(string search)
        {
            return utOfWork.CongeRepository.GetMany(x => x.type.Contains(search));
        }
        public IEnumerable<Conge> GetCongeByStatut(string search)
        {
            return utOfWork.CongeRepository.GetMany(x => x.Statut==search);
        }


        public IEnumerable<Conge> GetCongeThisUser(string user)
        {
            return utOfWork.CongeRepository.GetAlll(user);
        }
        public IEnumerable<Conge> GetCongeB(string search)
        {
            return utOfWork.CongeRepository.GetMany(x => x.Userid.Contains(search));
        }
        public IEnumerable<Solde> GetSoldeB(string search)
        {
            return utOfWork.SoldeRepository.GetMany(x => x.Userid.Contains(search));
        }
        public void solde(Solde s)
        //[Bind(Include ="DateDeb, DateFin, type, CommentaireC")]

        {
            // c.Userid =User.Identity.Name;

            utOfWork.SoldeRepository.Add(s);
            utOfWork.Commit();
        }
        public IEnumerable<Solde> GetSoldeByUser(string user)
        {
            return utOfWork.SoldeRepository.GetMany(x => x.Userid == user);
        }

        public void Dispose()
        {
            utOfWork.Dispose();
        }

    }
}
