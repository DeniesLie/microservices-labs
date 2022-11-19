using Microsoft.EntityFrameworkCore;
using TransactionService.Data;
using TransactionService.Data.Repositories.Implementations;
using TransactionService.Data.Repositories.Interfaces;
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
        services.AddTransient<ITransactionService, Services.Implementations.TransactionService>();
    }
    
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IStorageRepository, StorageRepository>();
    }
}