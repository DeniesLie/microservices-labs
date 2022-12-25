using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
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
