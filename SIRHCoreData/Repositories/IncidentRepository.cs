using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;


namespace SIRHCoreData.Repositories
{
    public class IncidentRepository : RepositoryBase<Incident>, IIncidentRepository

    {
        public IncidentRepository(IDatabaseFactory dbFactory)
                   : base(dbFactory)
        {
        }

    }
    public interface IIncidentRepository : IRepository<Incident>
    {
        
    }

}
