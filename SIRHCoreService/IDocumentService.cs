using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIRHCoreDomain;

namespace SIRHCoreService
{
  public  interface IDocumentService : IDisposable
    {
        void createDocument(Document d);
        //Document GetDocument(int id);

        IEnumerable<Document> GetDocuments();
        //void telechargerDocument(Document d);
        void DeleteDocument(Document d);
        void traiterDcocument([Bind("id")] Document d);

        //IEnumerable<Document> GetDocumentBytype(string type);
        //IEnumerable<Incident> GetDocumentBypropritere(string prpritere);
        
    }
}
