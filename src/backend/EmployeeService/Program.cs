using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using EmployeeService.DI;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Host.UseSerilog((context, config) =>
{
    config.Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["Elasticsearch:Uri"]))
        {
            IndexFormat = $"{context.Configuration["AppName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfShards = 2,
            NumberOfReplicas = 1
        })
        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
        .ReadFrom.Configuration(context.Configuration);
});

// Monitoring
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
