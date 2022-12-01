using TransactionService.Dtos;

namespace TransactionService.Services.Interfaces;

public interface IItemService
{
    Task<ItemGetDto?> GetByIdAsync(Guid id);
}