using Microsoft.EntityFrameworkCore;
using TransactionService.Models;

namespace TransactionService.Data.Seeds;

public static class ModelBuilderSeedExtension
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionModel>().HasData(TestSeeds.GetTransactions());
        modelBuilder.Entity<Item>().HasData(TestSeeds.GetItems());
        modelBuilder.Entity<Storage>().HasData(TestSeeds.GetStorages());
    }
}