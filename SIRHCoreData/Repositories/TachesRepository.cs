using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRHCoreData.Repositories
{
    
    public class TachesRepository : RepositoryBase<Taches>, ITachesRepository
    {
        public TachesRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }

    }


    public interface ITachesRepository : IRepository<Taches>
    {

    }
}
