using Microsoft.EntityFrameworkCore;
using StorageService.Models;

namespace StorageService.Data.DbExtensions;

public static class ModelBuilderExtension
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Storage>().HasData(TestSeedData.Storages);
    }
}