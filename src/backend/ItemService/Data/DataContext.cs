using ItemService.Models;

namespace ItemService.Data;

public class DataContext
{
    public List<Item> Items { get; set; } = new List<Item>();

    public DataContext()
    {
        Items = Seed();
    }
    
    private List<Item> Seed()
    {
        return new List<Item>()
        {
            new Item()
            {
                Id = 1,
                Name = "Javelin",
                Amount = 5
            },
            new Item()
            {
                Id = 2,
                Name = "Tank",
                Amount = 10
            },
            new Item()
            {
                Id = 3,
                Name = "Dry ration",
                Amount = 100
            }
        };
    }
}