using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TraceRouter.Core.Domain.Entities;
using TraceRouter.Core.Interfaces;

namespace TraceRouter.Infrastructure.Notifiers
{
    public class SignalRNotifier : IRealTimeNotifier
    {
        #region attributes
        private readonly IHubContext<Hub> _hubContext;
        #endregion
        #region constructor
        public SignalRNotifier(IHubContext<Hub> hubContext)
        {
            _hubContext = hubContext;
        }
        #endregion
        #region methods
        public async Task SendHopUpdateAsync(string connectionId, TraceRouteEntry entry)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveHopUpdate", entry);
        }
        public async Task SendTraceCompleteAsync(string connectionId, string message)
        {
            await _hubContext.Clients.Clients(connectionId).SendAsync("TraceComplete", message);
        }
        public async Task SendErrorAsync(string connectionId, string errorMessage)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync("TraceError", errorMessage);
        }
        #endregion
    }
}
