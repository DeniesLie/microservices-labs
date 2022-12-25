using TransactionService.Dtos;

namespace TransactionService.Models;

public class Item : EntityBase
{
    public string Name { get; set; } = String.Empty;
    public IEnumerable<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();

    public Item()
    {
    }
    
    public Item(ItemGetDto dto)
    {
        Id = dto.Id;
        Name = dto.Name ?? "";
    }

    public Item(ItemPublishedDto dto)
    {
        Id = dto.Id;
        Name = dto.Name ?? "";
    }
}