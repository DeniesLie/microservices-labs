using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TransactionService.Data;
using TransactionService.Data.Repositories.Interfaces;
using TransactionService.Models;

namespace TransactionService.Data.Repositories.Implementations;

public abstract class BaseRepository<TEntity> : IRepository<TEntity>
    where TEntity: EntityBase
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }
    
    public IQueryable<TEntity> Query()
    {
        return _dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = Query();
        
        if (include is not null)
            query = include(query);
        
        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }
    public async Task EnsureEntityExistsAsync(TEntity entity)
    {
        var doesEntityExists = (await GetByIdAsync(entity.Id)) is not null;

        if (!doesEntityExists)
        {
            Insert(entity);
        }

        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}