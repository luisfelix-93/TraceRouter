using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceRouter.Core.Domain.Entities;
using TraceRouter.Core.Interfaces;

namespace TraceRouter.Client.Notifiers
{
    public class ConsoleNotifier : IRealTimeNotifier
    {
        public  Task SendHopUpdateAsync(string connectionId, TraceRouteEntry entry)
        {
            Console.WriteLine(entry.ToString());
            return Task.CompletedTask;
        }
        public Task SendTraceCompleteAsync(string connectionId, string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
            return Task.CompletedTask;
        }
        public Task SendErrorAsync(string connectionId, string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERRO: {errorMessage}");
            Console.ResetColor();
            return Task.CompletedTask;
        }
    }
}
