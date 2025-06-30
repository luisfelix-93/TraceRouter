using TraceRouter.Client.Notifiers;
using TraceRouter.Core.UseCase;
using TraceRouter.Infrastructure.TraceRunners;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("---- Cliente de testes de TraceRoute via CLI ----");

        // Pega o Host a partir dos argumentos da linha de comando, ou usa um padrão
        string host = "www.google.com";
        if (args.Length > 0)
        {
            host = args[0];
        }
        else
        {
            Console.WriteLine("Uso: dotnet run <hostname>");
            Console.WriteLine($"Nenhum host fornecido. Usando o padrão: {host}\n");
        }
        Console.WriteLine($"Iniciando o rastreamento para: {host}\n");


        var tracerouterRunner = new PingTracerouterRunner();
        var consoleNotifier = new ConsoleNotifier();
        var startTraceRouteUseCase = new StartTraceUseCase(tracerouterRunner, consoleNotifier);

        try
        {
            string dummyConnection = "cli-session";
            await startTraceRouteUseCase.ExecuteAsync(host, dummyConnection);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\nOcorreu uma exceção fatal: {ex.Message}");
            Console.ResetColor();
        }
    }
}