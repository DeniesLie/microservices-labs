using TransactionService.Enums;
using TransactionService.Models;

namespace TransactionService.Dtos;

public class TransactionGetDto
{
    public Guid Id { get; set; }
    public TransactionType Type { get; set; }
    public int Amount { get; set; }
    public ItemGetDto? Item { get; set; }
    public Guid StorageId { get; set; }
    public string? Notes { get; set; }

    public TransactionGetDto(TransactionModel transaction)
    {
        Id = transaction.Id;
        Type = transaction.Type;
        Amount = transaction.Amount;
        StorageId = transaction.StorageId;
        Notes = transaction.Notes;
        
        if (transaction.Item is not null)
            Item = new ItemGetDto(transaction.Item);
    }
}