namespace ItemService.Models;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int? MeasureUnitId { get; set; }
}