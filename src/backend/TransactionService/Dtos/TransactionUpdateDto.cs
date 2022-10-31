namespace TransactionService.Dtos;

public class TransactionUpdateDto
{
    public Guid Id { get; set; }
    public string? Notes { get; set; }
}