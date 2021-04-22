using System;
using System.Collections.Generic;
using System.Text;

namespace SIRHCoreData.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        SIRHcontext DataContext { get; }
    }

}
