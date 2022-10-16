namespace ItemService.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Amount { get; set; }
    public int? MeasureUnitId { get; set; }
}