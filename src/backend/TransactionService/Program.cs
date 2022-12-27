using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using TransactionService.BackgroundServices;
using TransactionService.Data;
using TransactionService.Data.PrerpDb;
using TransactionService.DependencyInjection;
using TransactionService.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
      builder =>
      {
          builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
      });
});
builder.Services.AddEfDbContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddHttpClient();
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAllPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
