using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TraceRouter.Core.Domain.Entities;
using TraceRouter.Core.Interfaces;

namespace TraceRouter.Infrastructure.TraceRunners
{
    public class PingTracerouterRunner : ITracerouteRunner
    {
        private const int Timeout = 4000;
        private static readonly byte[] Buffer = Encoding.ASCII.GetBytes("TraceRoutePacket");

        public async Task<TraceRouteEntry> RunHopAsync(string destinationAddres, int ttl)
        {
            using var pingSender = new Ping();
            var pingOptions = new PingOptions(ttl, true);
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            var reply = await pingSender.SendPingAsync(destinationAddres, Timeout, Buffer, pingOptions);
            stopWatch.Stop();


            string hostname = "";
            if (reply.Address != null)
            {
                try
                {
                   var dnsTask = Dns.GetHostEntryAsync(reply.Address);
                    if (await Task.WhenAny(dnsTask, Task.Delay(500)) == dnsTask)
                    {
                        hostname = dnsTask.Result.HostName;
                    }
                }
                catch { /* Igonora falha na resolução do DNS reverso */ }
            }

            return new TraceRouteEntry
            {
                Hop = ttl,
                Adress = reply.Address?.ToString() ?? "*",
                Hostname = hostname,
                Latency = stopWatch.ElapsedMilliseconds,
                Status = reply.Status
            };

        }
    }
}
