namespace ItemService.DTOs;

public class ItemGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}