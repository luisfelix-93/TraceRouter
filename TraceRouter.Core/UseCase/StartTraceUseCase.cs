using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TraceRouter.Core.Interfaces;

namespace TraceRouter.Core.UseCase
{
    public class StartTraceUseCase
    {
        #region attributes
        private const int MaxHops = 30;
        private readonly ITracerouteRunner _runner;
        private readonly IRealTimeNotifier _notifier;
        #endregion
        #region constructor
        public StartTraceUseCase(ITracerouteRunner runner, IRealTimeNotifier notifier)
        {
            _runner = runner;
            _notifier = notifier;
        }
        #endregion
        #region methods
        public async Task ExecuteAsync(string hostNameOrAdress, string connectionId)
        {
            IPAddress destinationIp;
            try
            {
                var hostEntry = await Dns.GetHostEntryAsync(hostNameOrAdress);
                destinationIp = hostEntry.AddressList[0];
            }
            catch (Exception ex)
            {
                await _notifier.SendErrorAsync(connectionId, $"Falha ao resolver o host: {ex.Message}");
                return;
            }

            try
            {
                for (int ttl = 1; ttl <= MaxHops; ttl++)
                {
                    var entry = await _runner.RunHopAsync(destinationIp.ToString(), ttl);
                    await _notifier.SendHopUpdateAsync(connectionId, entry);

                    if (entry.Status == System.Net.NetworkInformation.IPStatus.Success)
                    {
                        break;
                    }
                }
                await _notifier.SendTraceCompleteAsync(connectionId, "Rastreamento concluido com sucesso");
            }
            catch (Exception ex)
            {
                await _notifier.SendErrorAsync(connectionId, $"Erro ao executar o rastreamento: {ex.Message}");
            }
        }
        #endregion
    }
}
