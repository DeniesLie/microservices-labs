using Microsoft.AspNetCore.Mvc;
using TransactionService.Dtos;
using TransactionService.Models;

namespace TransactionService.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Transaction> GetAllForStorage()
        {
            return new List<Transaction>()
            {
                new Transaction()
                {
                    Id = 1,
                    Name = "Supply",
                    ItemName = "Javelin",
                    ItemAmount = 305,
                    StorageId = 4
                },
                new Transaction()
                {
                    Id = 2,
                    Name = "Supply",
                    ItemName = "NLAW",
                    ItemAmount = 204,
                    StorageId = 4
                },
                new Transaction()
                {
                    Id = 3,
                    Name = "Write-off",
                    ItemName = "Switchblade 300",
                    ItemAmount = 105,
                    StorageId = 4
                }
            };
        }

        [HttpGet("{transactionId:int:min(1)}")]
        public Transaction GetById(int transactionId)
        {
            return new Transaction()
            {
                Id = 1,
                Name = "Supply",
                ItemName = "Javelin",
                ItemAmount = 305,
                StorageId = 4
            };
        }

        [HttpPost("supply")]
        public IActionResult Supply([FromBody] SupplyDto supplyDto)
        {
            return Ok(new Transaction()
            {
                Id = 1,
                Name = "Supply",
                ItemName = "Javelin",
                ItemAmount = 305,
                StorageId = 4
            });
        }
    }
}
