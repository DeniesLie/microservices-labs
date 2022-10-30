using ItemService.DTOs;

namespace ItemService.Services.Abstract;

public interface IItemService
{
    Task<ItemGetDto> CreateAsync(ItemPostDto itemPostDto);
    Task<List<ItemGetDto>> GetAllAsync();
    Task<ItemGetDto> GetByIdAsync(Guid id);
    Task<ItemGetDto> UpdateAsync(ItemPutDto itemPutDto);
    Task DeleteAsync(Guid id);
}