namespace TransactionService.Dtos;

public class StorageGetDto
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }
}