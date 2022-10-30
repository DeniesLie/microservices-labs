using TransactionService.Data.Repositories.Interfaces;
using TransactionService.Models;

namespace TransactionService.Data.Repositories.Implementations;

public class TransactionRepository : BaseRepository<TransactionModel>, ITransactionRepository
{
    public TransactionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}