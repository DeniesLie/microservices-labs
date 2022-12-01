using Microsoft.EntityFrameworkCore.Query;
using TransactionService.Models;

namespace TransactionService.Data.Repositories.Interfaces;

public interface IRepository<TEntity>
    where TEntity: EntityBase
{
    IQueryable<TEntity> Query();
    Task<TEntity?> GetByIdAsync(Guid id,Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    void Insert(TEntity entity);
    void Delete(TEntity entity);
    void Update(TEntity entity);
    Task EnsureEntityUpdatedAsync(TEntity entity);
    Task SaveChangesAsync();
}