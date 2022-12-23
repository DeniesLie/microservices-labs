using TransactionService.Dtos;

namespace TransactionService.Models;

public class Storage : EntityBase
{
    public IEnumerable<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();

    public Storage()
    {
    }
    
    public Storage(StorageGetDto dto)
    {
        Id = dto.Id;
    }

    public Storage(StoragePublishedDto dto)
    {
        Id = dto.Id;
    }
}