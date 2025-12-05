using Lib.Core.Domain;
using System.Linq.Expressions;

namespace App.Persistance.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(long id);
        Task<List<TEntity>> ToListAsync();
        Task DeleteAsync(TEntity entity);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);

    }
}
