using SIRHCoreData.Infrastructure;
using SIRHCoreDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIRHCoreData.Repositories
{
    public class NoteDeFraisRepository : RepositoryBase<NoteDeFrais>, INoteDeFraisRepository
    {
        public NoteDeFraisRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }

    }
    public interface INoteDeFraisRepository : IRepository<NoteDeFrais> { }

}