namespace ItemService.DTOs;

public class ItemPutDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}