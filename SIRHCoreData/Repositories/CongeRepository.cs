using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIRHCoreData.Repositories
{
    public class CongeRepository : RepositoryBase<Conge>, ICongeRepository
    {
        public CongeRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }

    }
    public interface ICongeRepository : IRepository<Conge>
    {
      
    }

}
