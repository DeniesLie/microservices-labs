using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionService.Data.Repositories.Interfaces;
using TransactionService.Dtos;
using TransactionService.Models;

namespace TransactionService.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IStorageRepository _storageRepo;
        private readonly IItemRepository _itemRepo;
        
        public TransactionsController(
            ITransactionRepository transactionRepo,
            IStorageRepository storageRepo, 
            IItemRepository itemRepo)
        {
            _transactionRepo = transactionRepo;
            _storageRepo = storageRepo;
            _itemRepo = itemRepo;
        }

        [HttpGet("{transactionId:guid}", Name = "GetById")]
        public async Task<ActionResult<TransactionGetDto>> GetByIdAsync(Guid transactionId)
        {
            var transaction = await _transactionRepo.GetByIdAsync(
                transactionId, 
                include: q => q.Include(t => t.Item!));

            if (transaction is null)
                return NotFound();

            var result = new TransactionGetDto(transaction);

            return result;
        }
        
        [HttpGet("getByStorageId/{storageId:guid}")]
        public async Task<ActionResult<IEnumerable<TransactionGetDto>>> 
            GetAllForStorageAsync(Guid storageId)
        {
            var storage = await _storageRepo.GetByIdAsync(storageId);

            if (storage is null)
                return NotFound();

            var result = await _transactionRepo.Query()
                .Where(t => t.StorageId == storageId)
                .Include(t => t.Item)
                .Select(t => new TransactionGetDto(t))
                .ToListAsync();

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<TransactionGetDto>> CreateAsync([FromBody] TransactionPostDto transactionDto)
        {
            var storage = await _storageRepo.GetByIdAsync(transactionDto.StorageId);
            var item = await _itemRepo.GetByIdAsync(transactionDto.ItemId);

            if (storage is null || item is null)
                return NotFound();
                
            var transaction = new TransactionModel(transactionDto);
            
            _transactionRepo.Insert(transaction);
            await _transactionRepo.SaveChangesAsync();

            var result = new TransactionGetDto(transaction);
            
            return CreatedAtRoute("GetById", 
                new { transactionId = result.Id }, 
                result);
        }

        [HttpPut]
        public async Task<ActionResult<TransactionGetDto>> UpdateAsync([FromBody] TransactionUpdateDto transactionDto)
        {
            var transaction = await _transactionRepo.GetByIdAsync(
                transactionDto.Id,
                include: q => q.Include(t => t.Item!));

            if (transaction is null)
                return NotFound();

            transaction.Notes = transactionDto.Notes;
            await _transactionRepo.SaveChangesAsync();

            var result = new TransactionGetDto(transaction);
            return result;
        }

        [HttpDelete("{transactionId:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid transactionId)
        {
            var transaction = await _transactionRepo.GetByIdAsync(transactionId);

            if (transaction is null)
                return NotFound();

            _transactionRepo.Delete(transaction);
            await _transactionRepo.SaveChangesAsync();
            return Ok();
        }
    }
}
