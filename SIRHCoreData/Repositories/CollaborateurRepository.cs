using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRHCoreData.Repositories
{
    public class CollaborateurRepository : RepositoryBase<Collaboration>, ICollaborateurRepository
    {
        public CollaborateurRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
    {
    }

}


public interface ICollaborateurRepository : IRepository<Collaboration>
{

}
 
}
