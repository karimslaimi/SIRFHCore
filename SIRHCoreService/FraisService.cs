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
    public class FraisService : IFraisService

    {


        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        public FraisService()
        {
            dbFactory = new DatabaseFactory();
            utOfWork = new UnitOfWork(dbFactory);
        }


        public IEnumerable<NoteDeFrais> GetFrais()
        {
            var Frais = utOfWork.NoteDeFraisRepository.GetAll();
            return Frais;
        }
        public void CreateFrais(NoteDeFrais f)
      
        {
           

            utOfWork.NoteDeFraisRepository.Add(f);
            utOfWork.Commit();
        }
      
        public void UpdateFrais(NoteDeFrais f)
        {
            utOfWork.NoteDeFraisRepository.Update(f);
            utOfWork.Commit();
        }
      

        public void traiterFrais([Bind("Statut")] NoteDeFrais f)
        {
            utOfWork.NoteDeFraisRepository.Update(f);
            utOfWork.Commit();
        }
        public void DeleteFrais(NoteDeFrais f)
        {
            utOfWork.NoteDeFraisRepository.Delete(f);
            utOfWork.Commit();
        }
       public NoteDeFrais GetFrais(int NoteDeFraisID)
        {
            var Frais = utOfWork.NoteDeFraisRepository.GetById(NoteDeFraisID);
            return Frais;
        }
        public NoteDeFrais GetFrais(string Userid)
        {
            var Frais = utOfWork.NoteDeFraisRepository.GetById(Userid);
            return Frais;
        }
        public IEnumerable<NoteDeFrais> GetFraisByUser(string user)
        {
            return utOfWork.NoteDeFraisRepository.GetMany(x => x.Userid == user);
        }
        public IEnumerable<NoteDeFrais> GetFraisByStatut(string search)
        {
            return utOfWork.NoteDeFraisRepository.GetMany(x => x.Statut.Contains(search));
        }


       /* public IEnumerable<NoteDeFrais> GetFraisThisUser(string user)
        {
            return utOfWork.NoteDeFraisRepository.GetAlll(user);
        }*/
        public IEnumerable<NoteDeFrais> GetFraisB(string search)
        {
            return utOfWork.NoteDeFraisRepository.GetMany(x => x.Userid.Contains(search));
        }


        public void Dispose()
        {
            utOfWork.Dispose();
        }

    }
}
