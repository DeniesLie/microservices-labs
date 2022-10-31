using ItemService.Data;
using ItemService.DTOs;
using ItemService.Models;
using ItemService.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Services.Implementation;

public class ItemService: IItemService
{
    private readonly ItemContext _itemContext;

    public ItemService(ItemContext itemContext)
    {
        _itemContext = itemContext;
    }
    
    public async Task<ItemGetDto> CreateAsync(ItemPostDto itemPostDto)
    {
        var newItem = new Item()
        {
            Id = Guid.NewGuid(),
            Name = itemPostDto.Name
        };
        
        _itemContext.Items.Add(newItem);
        await _itemContext.SaveChangesAsync();

        var createdItem = await GetByIdAsync(newItem.Id);
        return createdItem;
    }

    public async Task<List<ItemGetDto>> GetAllAsync()
    {
        var items = await _itemContext.Items
            .Select(i => new ItemGetDto()
            {
                Id = i.Id,
                Name = i.Name
            })
            .ToListAsync();

        return items;
    }

    public async Task<ItemGetDto> GetByIdAsync(Guid id)
    {
        var item = await _itemContext.Items
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item is null)
        {
            throw new ArgumentException("Item with such an id was not found", nameof(id));
        }

        var result = new ItemGetDto()
        {
            Id = item.Id,
            Name = item.Name
        };

        return result;
    }

    public async Task<ItemGetDto> UpdateAsync(ItemPutDto itemPutDto)
    {
        var existingItem = await _itemContext.Items
            .FirstOrDefaultAsync(i => i.Id == itemPutDto.Id);
        
        if (existingItem is null)
        {
            throw new ArgumentException("Item with such an id was not found", nameof(itemPutDto.Id));
        }

        existingItem.Name = itemPutDto.Name;

        _itemContext.Items.Update(existingItem);
        await _itemContext.SaveChangesAsync();

        return await GetByIdAsync(existingItem.Id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var existingItem = await _itemContext.Items
            .FirstOrDefaultAsync(i => i.Id == id);
        
        if (existingItem is null)
        {
            throw new ArgumentException("Item with such an id was not found", nameof(id));
        }

        _itemContext.Items.Remove(existingItem);
        await _itemContext.SaveChangesAsync();
    }
}