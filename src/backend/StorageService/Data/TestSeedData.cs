using StorageService.Models;

namespace StorageService.Data;

public static class TestSeedData
{
    public static List<Storage> Storages = new List<Storage>()
    {
        new Storage()
        {
            Id = Guid.Parse("03dec121-ac0e-4189-bd9e-68013cd175f1"),
            Name = "Storage A",
            Address = "Winners avenue, 10"
        },
        new Storage()
        {
            Id = Guid.Parse("7c164a1e-84ab-44b9-9542-9c0a681172a6"),
            Name = "Storage B",
            Address = "Magic street, 70/2"
        },
        new Storage()
        {
            Id = Guid.Parse("8f515945-5f20-4acb-95fc-b0aa2eb91490"),
            Name = "Storage C",
            Address = "Central square"
        }
    };
}
