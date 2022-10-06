using ItemService.Data;
using ItemService.DTOs;
using ItemService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.Controllers;

[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly DataContext _dataContext = new DataContext();
    
    [HttpGet]
    public IEnumerable<ItemGetDto> GetAll()
    {
        return _dataContext.Items.Select(i => new ItemGetDto()
        {
            Id = i.Id,
            Name = i.Name
        });
    }

    [HttpGet("{itemId:int}")]
    public ItemGetDto GetById(int itemId)
    {
        if (itemId < 0 || itemId > _dataContext.Items.Count)
        {
            var firstElement = _dataContext.Items[0];
            return new ItemGetDto {Id = firstElement.Id, Name = firstElement.Name};
        }

        return _dataContext.Items
            .Select(i => new ItemGetDto {Id = i.Id, Name = i.Name})
            .FirstOrDefault(i => i.Id == itemId)!;
    }

    [HttpPost]
    public ItemGetDto Create(ItemPostDto itemPostDto)
    {
        _dataContext.Items
            .Add(new Item()
            {
                Id = _dataContext.Items.Count + 1,
                Name = itemPostDto.Name
            });

        return _dataContext.Items.Select(i => new ItemGetDto {Id = i.Id, Name = i.Name})
            .LastOrDefault()!;
    }
}