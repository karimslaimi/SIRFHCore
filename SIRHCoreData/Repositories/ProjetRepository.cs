using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRHCoreData.Repositories
{
    public class ProjetRepository : RepositoryBase<Projet>, IProjetRepository
    {
        public ProjetRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }
    
    }
    public interface IProjetRepository : IRepository<Projet>
    {

    }
}
