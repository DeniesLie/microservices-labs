using Microsoft.EntityFrameworkCore;
using TransactionService.Data.PrerpDb;
using TransactionService.Models;
namespace TransactionService.Data;

public class AppDbContext : DbContext
{
    public DbSet<TransactionModel>? Transactions { get; set; }
    public DbSet<Item>? Items { get; set; }
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