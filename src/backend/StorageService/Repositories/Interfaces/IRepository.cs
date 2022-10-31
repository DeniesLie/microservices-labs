using Microsoft.EntityFrameworkCore.Query;
using StorageService.Models;

namespace StorageService.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : Model
{
    IQueryable<TEntity> Query();
    Task<TEntity?> GetByIdAsync(Guid id);
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveChangesAsync();
}
