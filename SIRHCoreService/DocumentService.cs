using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;


namespace SIRHCoreService
{
    public class DocumentService : IDocumentService
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public DocumentService()
        {
            dbFactory = new DatabaseFactory();
            utOfWork = new UnitOfWork(dbFactory);
        }
        public IEnumerable<Document> GetDocuments()
        {
            var Document = utOfWork.DocumentRepository.GetAll();
            return Document;
        }
        public void createDocument(Document d)

        {


            utOfWork.DocumentRepository.Add(d);
            utOfWork.Commit();
        }
        //public void telechargerdocument(Document d)
        //{
        //    utOfWork.DocumentRepository.TelechargerDocument(d);
        //    utOfWork.Commit();
        //}
        public void DeleteDocument(Document d)
        {
            utOfWork.DocumentRepository.Delete(d);
            utOfWork.Commit();
        }
        public void traiterDcocument(Document d)
        {
            utOfWork.DocumentRepository.Update(d);
            utOfWork.Commit();
        }
        //public Document getdocumentbytype(string type)
        //{
        //    var Document = utOfWork.DocumentRepository.GetByType(type);
        //    return (Document)Document;
        //}
        //public Document getdocumentbypropritere(string propritere)
        //{
        //    var Document = utOfWork.DocumentRepository.GetByPropritere(propritere);
        //    return (Document)Document;
        //}
        public void Dispose()
        {
            utOfWork.Dispose();
        }
    }
}
