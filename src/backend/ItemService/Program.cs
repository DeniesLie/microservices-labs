using ItemService;
using ItemService.Data;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddServices();

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
