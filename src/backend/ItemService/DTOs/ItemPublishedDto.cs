using System;

namespace ItemService.DTOs;

public class ItemPublishedDto 
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Event { get; set; }

    public ItemPublishedDto(ItemGetDto itemGetDto)
    {
        Id = itemGetDto.Id;
        Name = itemGetDto.Name;
    }
}