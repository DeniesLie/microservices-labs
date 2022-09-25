namespace TransactionService.Models;

public class Transaction
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ItemId { get; set; }
    public string? ItemName { get; set; }
    public int ItemAmount { get; set; }
    public int StorageId { get; set; }
}