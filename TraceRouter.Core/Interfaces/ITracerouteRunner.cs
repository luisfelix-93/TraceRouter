using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceRouter.Core.Domain.Entities;

namespace TraceRouter.Core.Interfaces
{
    public interface ITracerouteRunner
    {
        Task<TraceRouteEntry> RunHopAsync(string destinationAdress, int ttl);
    }
}
