using System;
using System.Collections.Generic;
using System.Text;

namespace SIRHCoreData.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private SIRHcontext dataContext;
        public SIRHcontext DataContext { get { return dataContext; } }

        public DatabaseFactory()
        {
            dataContext = new SIRHcontext();
        }
        protected override void DisposeCore()
        {
            if (DataContext != null)
                DataContext.Dispose();
        }
    }

}
