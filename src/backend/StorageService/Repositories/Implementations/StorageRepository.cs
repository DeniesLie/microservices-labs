using StorageService.Data;
using StorageService.Models;
using StorageService.Repositories.Interfaces;

namespace StorageService.Repositories.Implementations;

public class StorageRepository: BaseRepository<Storage>, IStorageRepository
{
    public StorageRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}