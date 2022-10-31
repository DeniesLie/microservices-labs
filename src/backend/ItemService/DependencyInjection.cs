using ItemService.Data;
using ItemService.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ItemService;

public static class DependencyInjection
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ItemContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });
    }
    
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IItemService, Services.Implementation.ItemService>();
    }
}