
using APP.Persistance.DbContexts;
using Lib.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Persistance.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase
    {
        private readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            _ = await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> GetByIdAsync(long id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            _ = await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> ToListAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)

                throw new ArgumentNullException(nameof(entity));
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

       
    }
}
