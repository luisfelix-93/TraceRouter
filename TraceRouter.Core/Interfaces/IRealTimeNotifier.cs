using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceRouter.Core.Domain.Entities;

namespace TraceRouter.Core.Interfaces
{
    public interface IRealTimeNotifier
    {
        Task SendHopUpdateAsync(string connectionId, TraceRouteEntry entry);
        Task SendTraceCompleteAsync(string connectionId, string message);
        Task SendErrorAsync(string connectionId, string errorMessage);
    }
}

