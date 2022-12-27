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
        private readonly ILogger _logger;

        public TransactionsController(
            ITransactionRepository transactionRepo,
            IStorageRepository storageRepo, 
            IItemRepository itemRepo, 
            ITransactionService transactionService,
            ILogger<TransactionsController> logger)
        {
            _transactionRepo = transactionRepo;
            _storageRepo = storageRepo;
            _itemRepo = itemRepo;
            _transactionService = transactionService;
            _logger = logger;
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
            
            _logger.LogInformation($"Transaction created. Id: {result.Id}");

            return CreatedAtRoute("GetById", 
                new { transactionId = result.Id }, 
                result);
        }

        [HttpPut]
        public async Task<ActionResult<TransactionGetDto>> UpdateAsync([FromBody] TransactionUpdateDto transactionDto)
        {
            var result = await _transactionService.UpdateAsync(transactionDto);

            _logger.LogInformation($"Transaction updated. Id: {result.Id}");

            return result;
        }

        [HttpDelete("{transactionId:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid transactionId)
        {
            await _transactionService.DeleteAsync(transactionId);

            _logger.LogInformation($"Transaction deleted. Id: {transactionId}");

            return Ok();
        }

        // Calls broken Endpoint for storage service to test retry/timeout & circuit breaker
        [HttpGet("testBrokenEndpoint")]
        public async Task<IActionResult> TestBrokenEndpointAsync() 
        {
            await _transactionService.TestBrokenEndpointAsync();
            _logger.LogInformation("Broken endpoint was successfully invoked from transaction service");

            return Ok();
        }
    }
}
