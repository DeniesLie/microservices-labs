using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using TransactionService.AsyncDataServices.Abstractions;
using TransactionService.AsyncDataServices.Consumers;
using TransactionService.BackgroundServices;
using TransactionService.Data;
using TransactionService.Data.Repositories.Implementations;
using TransactionService.Data.Repositories.Interfaces;
using TransactionService.Dtos;
using TransactionService.EventProcessors;
using TransactionService.Services.Implementations;
using TransactionService.Services.Interfaces;

namespace TransactionService.DependencyInjection;

public static class DiExtension
{
    public static void AddEfDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseSqlServer(config.GetConnectionString("Default"));
        });
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionService, Services.Implementations.TransactionService>();
        services.AddScoped<IStorageService, StorageHttpService>();
        services.AddScoped<IItemService, ItemHttpService>();
        
        services.AddSingleton<IEventProcessor, EventProcessor>();
        services.AddSingleton<IMessageBusConsumer<ItemPublishedDto>, ItemConsumer>();
        services.AddSingleton<IMessageBusConsumer<StoragePublishedDto>, StorageConsumer>();
        services.AddHostedService<MessageBusListener>();

        services.Configure<KestrelServerOptions>(opts => { opts.AllowSynchronousIO = true; });
        services.AddMetrics();
    }
    
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IStorageRepository, StorageRepository>();
    }
}