using SIRHCoreData.Repositories;
using SIRHCoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRHCoreService
{
    public interface ITachesService:ITachesRepository
    {
        List<Taches> GetTachesbyProjct(int id);
        Taches GetTaches(int id);
    }
}
