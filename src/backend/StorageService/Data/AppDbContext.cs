using Microsoft.EntityFrameworkCore;
using StorageService.Models;
using StorageService.Data.DbExtensions;

namespace StorageService.Data;

public class AppDbContext: DbContext
{
    public DbSet<Storage>? Storages { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.SeedData();
    }
}