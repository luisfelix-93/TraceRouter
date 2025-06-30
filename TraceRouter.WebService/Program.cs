using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using TraceRouter.Core.Interfaces;
using TraceRouter.Core.UseCase;
using TraceRouter.Infrastructure.Notifiers;
using TraceRouter.Infrastructure.TraceRunners;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials();
        });
});

builder.Services.AddTransient<ITracerouteRunner, PingTracerouterRunner>();
builder.Services.AddTransient<IRealTimeNotifier, SignalRNotifier>();
builder.Services.AddTransient<StartTraceUseCase>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapHealthChecks("/health"); // <--- Endpoint para checagem de health
// Edpoint para monitoramento
app.MapHealthChecks("/health-details", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        var result = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description
            })
        };
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(result);
    }
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
