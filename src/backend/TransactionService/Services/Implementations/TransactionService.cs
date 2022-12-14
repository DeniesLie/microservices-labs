using Microsoft.EntityFrameworkCore;
using TransactionService.Data.Repositories.Interfaces;
using TransactionService.Dtos;
using TransactionService.Exceptions;
using TransactionService.Models;
using TransactionService.Services.Interfaces;

namespace TransactionService.Services.Implementations;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepo;
    private readonly IItemRepository _itemRepo;
    private readonly IStorageRepository _storageRepo;
    
    private readonly IStorageService _storageService;
    private readonly IItemService _itemService;

    public TransactionService(
        ITransactionRepository transactionRepo, 
        IStorageService storageService, 
        IItemService itemService, 
        IItemRepository itemRepo, 
        IStorageRepository storageRepo)
    {
        _transactionRepo = transactionRepo;
        _storageService = storageService;
        _itemService = itemService;
        _itemRepo = itemRepo;
        _storageRepo = storageRepo;
    }

    public async Task<TransactionGetDto> GetByIdAsync(Guid transactionId)
    {
        var transaction = await _transactionRepo.GetByIdAsync(
            transactionId, 
            include: q => q.Include(t => t.Item!));

        if (transaction is null)
            throw new NotFoundException("transaction not found");

        var result = new TransactionGetDto(transaction);

        return result;
    }

    public async Task<IEnumerable<TransactionGetDto>> GetAllAsync()
    {
        var result = await _transactionRepo.Query()
            .Include(t => t.Item)
            .Select(t => new TransactionGetDto(t))
            .ToListAsync();

        return result;
    }

    public async Task<IEnumerable<TransactionGetDto>> GetAllForStorageAsync(Guid storageId)
    {
        var storage = await _storageService.GetByIdAsync(storageId);

        if (storage is null)
            throw new NotFoundException("Storage not found");

        var result = await _transactionRepo.Query()
            .Where(t => t.StorageId == storageId)
            .Include(t => t.Item)
            .Select(t => new TransactionGetDto(t))
            .ToListAsync();

        return result;
    }

    public async Task<TransactionGetDto> CreateAsync(TransactionPostDto transactionDto)
    {
        var storage = await _storageService.GetByIdAsync(transactionDto.StorageId);
        var item = await _itemService.GetByIdAsync(transactionDto.ItemId);

        if (storage is null)
            throw new NotFoundException("Storage not found");

        if (item is null)
            throw new NotFoundException("Item not found");

        await _storageRepo.EnsureEntityUpdatedAsync(new Storage(storage));
        await _itemRepo.EnsureEntityUpdatedAsync(new Item(item));
        
        var transaction = new TransactionModel(transactionDto);
            
        _transactionRepo.Insert(transaction);
        await _transactionRepo.SaveChangesAsync();

        var result = new TransactionGetDto(transaction);

        return result;
    }

    public async Task<TransactionGetDto> UpdateAsync(TransactionUpdateDto transactionDto)
    {
        var transaction = await _transactionRepo.GetByIdAsync(
            transactionDto.Id,
            include: q => q.Include(t => t.Item!));

        if (transaction is null)
            throw new NotFoundException("Transaction not found");

        transaction.Notes = transactionDto.Notes;
        await _transactionRepo.SaveChangesAsync();

        var result = new TransactionGetDto(transaction);
        return result;
    }

    public async Task DeleteAsync(Guid transactionId)
    {
        var transaction = await _transactionRepo.GetByIdAsync(transactionId);

        if (transaction is null)
            throw new NotFoundException("Transaction not found");

        _transactionRepo.Delete(transaction);
        await _transactionRepo.SaveChangesAsync();
    }
}