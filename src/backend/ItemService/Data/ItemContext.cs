using ItemService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Data;

public class ItemContext: DbContext
{
    public DbSet<Item> Items { get; set; }

    public ItemContext(DbContextOptions<ItemContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}