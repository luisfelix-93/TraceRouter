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
