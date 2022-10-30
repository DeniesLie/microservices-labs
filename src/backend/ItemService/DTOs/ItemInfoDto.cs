namespace ItemService.DTOs;

public class ItemInfoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Amount { get; set; }
}