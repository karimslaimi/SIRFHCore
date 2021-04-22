using System;
using System.Collections.Generic;
using System.Text;
using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;
namespace SIRHCoreData.Repositories
{
    public class SoldeRepository : RepositoryBase<Solde>, ISoldeRepository
    {
        public SoldeRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }

    }
    public interface ISoldeRepository : IRepository<Solde> { }

}
