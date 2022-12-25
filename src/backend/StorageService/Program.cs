using StorageService;
using StorageService.AsyncDataServices.Abstractions;
using StorageService.AsyncDataServices.Publishers;
using StorageService.Data.DbExtensions;
using StorageService.DI;
using StorageService.Dtos;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Logging
builder.Host.UseMetricsWebTracking()
    .UseMetrics(opts =>
    {
        opts.EndpointOptions = endpointOpts =>
        {
            endpointOpts.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
            endpointOpts.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
            endpointOpts.EnvironmentInfoEndpointEnabled = false;
        };
    });

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddSingleton<HealthState>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

builder.Services.Configure<KestrelServerOptions>(opts => { opts.AllowSynchronousIO = true; });
builder.Services.AddMetrics();
builder.Services.AddEfDbContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddSingleton<IMessageBusPublisher<StoragePublishedDto>, MessageBusStoragePublisher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
