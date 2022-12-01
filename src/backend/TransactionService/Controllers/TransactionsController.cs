using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionService.Data.Repositories.Interfaces;
using TransactionService.Dtos;
using TransactionService.Models;
using TransactionService.Services.Interfaces;

namespace TransactionService.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IStorageRepository _storageRepo;
        private readonly IItemRepository _itemRepo;
        private readonly ITransactionService _transactionService;

        public TransactionsController(
            ITransactionRepository transactionRepo,
            IStorageRepository storageRepo, 
            IItemRepository itemRepo, 
            ITransactionService transactionService)
        {
            _transactionRepo = transactionRepo;
            _storageRepo = storageRepo;
            _itemRepo = itemRepo;
            _transactionService = transactionService;
        }

        [HttpGet("{transactionId:guid}", Name = "GetById")]
        public async Task<ActionResult<TransactionGetDto>> GetByIdAsync(Guid transactionId)
        {
            var result = await _transactionService.GetByIdAsync(transactionId);
            return result;
        }
        
        [HttpGet]
        public async Task<IEnumerable<TransactionGetDto>> GetAllAsync()
        {
            var result = await _transactionService.GetAllAsync();
            return result;
        }
        
        [HttpGet("getByStorageId/{storageId:guid}")]
        public async Task<ActionResult<IEnumerable<TransactionGetDto>>> 
            GetAllForStorageAsync(Guid storageId)
        {
            var result = await _transactionService.GetAllForStorageAsync(storageId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionGetDto>> CreateAsync([FromBody] TransactionPostDto transactionDto)
        {
            var result = await _transactionService.CreateAsync(transactionDto);
            
            return CreatedAtRoute("GetById", 
                new { transactionId = result.Id }, 
                result);
        }

        [HttpPut]
        public async Task<ActionResult<TransactionGetDto>> UpdateAsync([FromBody] TransactionUpdateDto transactionDto)
        {
            var result = await _transactionService.UpdateAsync(transactionDto);
            return result;
        }

        [HttpDelete("{transactionId:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid transactionId)
        {
            await _transactionService.DeleteAsync(transactionId);
            return Ok();
        }
    }
}
