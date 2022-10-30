using ItemService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Data;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().HasData(SeedItems());
    }
    
    private static List<Item> SeedItems()
    {
        return new List<Item>()
        {
            new Item()
            {
                Id = Guid.Parse("03dec121-ac0e-4189-bd9e-68013cd175f9"),
                Name = "Javelin"
            },
            new Item()
            {
                Id = Guid.Parse("7c164a1e-84ab-44b9-9542-9c0a681172a7"),
                Name = "NLAW"
            },
            new Item()
            {
                Id = Guid.Parse("8f515945-5f20-4acb-95fc-b0aa2eb91497"),
                Name = "Switchblade 300"
            }
        };
    }
}