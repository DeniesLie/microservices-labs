using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using EmployeeService.DI;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Logs and monitoring
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
builder.Services.Configure<KestrelServerOptions>(opts => { opts.AllowSynchronousIO = true; });
builder.Services.AddMetrics();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddEfDbContext(builder.Configuration);
builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
