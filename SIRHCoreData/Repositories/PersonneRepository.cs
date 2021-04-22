using System;
using System.Collections.Generic;
using System.Text;
using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;

namespace SIRHCoreData.Repositories
{
    public class PersonneRepository : RepositoryBase<Personne>, IPersonneRepository
    {
        public PersonneRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }

    }
    public interface IPersonneRepository : IRepository<Personne> { }

}
