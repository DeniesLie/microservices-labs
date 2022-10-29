using TransactionService.Enums;

namespace TransactionService.Dtos;

public class TransactionPostDto
{
    public TransactionType Type { get; set; }
    public Guid ItemId { get; set; }
    public int Amount { get; set; }
    public Guid StorageId { get; set; }
    public string? Notes { get; set; }
}