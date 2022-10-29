using TransactionService.Data.Repositories.Interfaces;
using TransactionService.Models;

namespace TransactionService.Data.Repositories.Implementations;

public class StorageRepository : BaseRepository<Storage>, IStorageRepository
{
    public StorageRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}