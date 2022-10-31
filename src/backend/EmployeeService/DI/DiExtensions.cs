using EmployeeService.Data;
using EmployeeService.Repositories;
using EmployeeService.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.DI
{
    public static class DiExtensions
    {
        public static void AddEfDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<EmployeeContext>(opts =>
            {
                opts.UseSqlServer(config.GetConnectionString("Default"));
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
        }
    }
}
