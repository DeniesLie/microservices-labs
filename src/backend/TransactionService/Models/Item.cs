namespace TransactionService.Models;

public class Item : EntityBase
{
    public string Name { get; set; } = String.Empty;
    public IEnumerable<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
}