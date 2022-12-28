using EmployeeService.Auxiliary;
using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Data
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
