using Microsoft.EntityFrameworkCore;
using StorageService.Data;
using StorageService.Repositories.Implementations;
using StorageService.Repositories.Interfaces;

namespace StorageService.DI;

public static class DiExtension
{
    public static void AddEfDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseSqlServer(config.GetConnectionString("Default"));
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStorageRepository, StorageRepository>();
    }
}
