using TransactionService.Enums;
using TransactionService.Models;

namespace TransactionService.Data.PrerpDb;

public static class TestSeeds
{
    public static List<TransactionModel> GetTransactions()
    {
        return new List<TransactionModel>()
        {
            new TransactionModel()
            {
                Id = Guid.Parse("1cf2eaab-050f-471c-b45f-cd082efcf8bd"),
                Type = TransactionType.Income,
                ItemId = GetItems().First(i => i.Name == "Javelin").Id,
                StorageId = GetStorages()[0].Id,
                Amount = 204
            },
            new TransactionModel()
            {
                Id = Guid.Parse("84dbec22-7dc7-4319-9735-88029331fc8d"),
                Type = TransactionType.Income,
                ItemId = GetItems().First(i => i.Name == "NLAW").Id,
                StorageId = GetStorages()[1].Id,
                Amount = 103,
            },
            new TransactionModel()
            {
                Id = Guid.Parse("e6517314-65d4-407e-9d77-f148300701bc"),
                Type = TransactionType.Expense,
                ItemId = GetItems().First(i => i.Name == "Switchblade 300").Id,
                StorageId = GetStorages()[0].Id,
                Amount = 20
            },
        };
    }

    public static List<Item> GetItems()
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

    public static List<Storage> GetStorages()
    {
        return new List<Storage>()
        {
            new Storage()
            {
                Id = Guid.Parse("15ec584d-b6fb-4fd6-b064-76cc9e71f2e5")
            },
            new Storage()
            {
                Id = Guid.Parse("a872e805-9c4e-4589-af4c-8ed0af421b4c")
            }
        };
    }
}