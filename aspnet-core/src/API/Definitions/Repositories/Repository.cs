using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Definitions.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly TasksDbContext _context;

        public Repository(TasksDbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(condition);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception if needed
                throw;
            }

            return entity;
        }

        public async Task DeleteManyAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                return;
            }

            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                return;
            }

            _context.Set<TEntity>().AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();
            return Task.FromResult(queryable);
        }

        public Task<int> CountAsync()
        {
            return _context.Set<TEntity>().CountAsync();
        }

        private async Task<bool> EntityExists(TKey id)
        {
            return await _context.Set<TEntity>().AnyAsync(e => EqualityComparer<TKey>.Default.Equals((TKey)e.GetType().GetProperty("Id").GetValue(e), id));
        }
    }
}