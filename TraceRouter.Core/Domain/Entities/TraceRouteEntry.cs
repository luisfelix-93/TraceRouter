using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TraceRouter.Core.Domain.Entities
{
    public class TraceRouteEntry
    {
        public int Hop { get; set; }
        public string Adress { get; set; }
        public string Hostname { get; set; }
        public long Latency { get; set; }
        public IPStatus Status { get; set; }

        public override string ToString()
        {
            string addressPart = string.IsNullOrEmpty(Hostname) || Hostname == Adress
                ? $"[{Adress}]"
                : $"{Hostname} [{Adress}]";
            string latencyString = Status == IPStatus.TimedOut ? " * " : $"{Latency,4} ms";
            return $"{Hop,2} {latencyString} {addressPart}";
        }
    }
}
