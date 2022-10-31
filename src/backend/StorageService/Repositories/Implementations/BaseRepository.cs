using Microsoft.EntityFrameworkCore;
using StorageService.Data;
using StorageService.Models;
using StorageService.Repositories.Interfaces;

namespace StorageService.Repositories.Implementations;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Model
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public IQueryable<TEntity> Query() => _dbContext.Set<TEntity>();

    public void Create(TEntity entity) => _dbSet.Add(entity);

    public void Update(TEntity entity) => _dbSet.Update(entity);

    public void Delete(TEntity entity) => _dbSet.Remove(entity);

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        var query = Query();
        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}
