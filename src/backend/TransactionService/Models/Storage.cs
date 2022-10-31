namespace TransactionService.Models;

public class Storage : EntityBase
{
    public IEnumerable<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
}