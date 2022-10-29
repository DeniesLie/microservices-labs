using TransactionService.Dtos;
using TransactionService.Enums;

namespace TransactionService.Models;

public class TransactionModel : EntityBase
{
    public TransactionType Type { get; set; }
    public int Amount { get; set; }

    public string? Notes { get; set; }
    
    public Guid ItemId { get; set; }
    public Item? Item { get; set; }
    
    public Guid StorageId { get; set; }
    public Storage? Storage { get; set; }

    public TransactionModel()
    {
    }

    public TransactionModel(TransactionPostDto transactionPostDto)
    {
        Id = Guid.NewGuid();
        Type = transactionPostDto.Type;
        ItemId = transactionPostDto.ItemId;
        Amount = transactionPostDto.Amount;
        StorageId = transactionPostDto.StorageId;
        Notes = transactionPostDto.Notes;
    }
}