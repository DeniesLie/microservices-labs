using TransactionService.Models;

namespace TransactionService.Dtos;

public class ItemGetDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public ItemGetDto(Item item)
    {
        Id = item.Id;
        Name = item.Name;
    }
}