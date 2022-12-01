using TransactionService.Dtos;

namespace TransactionService.Services.Interfaces;

public interface ITransactionService
{
    Task<TransactionGetDto> GetByIdAsync(Guid transactionId);
    Task<IEnumerable<TransactionGetDto>> GetAllAsync();
    Task<IEnumerable<TransactionGetDto>> GetAllForStorageAsync(Guid storageId);
    Task<TransactionGetDto> CreateAsync(TransactionPostDto transactionPostDto);
    Task<TransactionGetDto> UpdateAsync(TransactionUpdateDto transactionUpdateDto);
    Task DeleteAsync(Guid transactionId);
}