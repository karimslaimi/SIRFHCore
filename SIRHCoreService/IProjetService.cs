using SIRHCoreData.Infrastructure;
using SIRHCoreData.Repositories;
using SIRHCoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIRHCoreService
{
    public interface IProjetService : IProjetRepository
    {

        Projet GetProjet(int id);
      
         
    }
}
