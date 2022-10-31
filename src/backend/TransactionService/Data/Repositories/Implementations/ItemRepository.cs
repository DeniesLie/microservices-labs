using TransactionService.Data.Repositories.Interfaces;
using TransactionService.Models;

namespace TransactionService.Data.Repositories.Implementations;

public class ItemRepository : BaseRepository<Item>, IItemRepository
{
    public ItemRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}