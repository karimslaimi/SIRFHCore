using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;

namespace SIRHCoreData.Repositories
{
    class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        public DocumentRepository(IDatabaseFactory dbFactory)
                          : base(dbFactory)
        {
        }
        
    }
    public interface IDocumentRepository : IRepository<Document> { }
    }
