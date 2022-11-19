using TransactionService.Dtos;

namespace TransactionService.Services.Interfaces;

public interface IStorageService
{
    Task<StorageGetDto?> GetByIdAsync(Guid id);
}