using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Definitions.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        Task<List<TEntity>> GetListAsync();
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<IQueryable<TEntity>> GetQueryableAsync();
        Task DeleteManyAsync(IEnumerable<TEntity> entities);
        Task InsertManyAsync(IEnumerable<TEntity> entities);
        Task<int> CountAsync();
    }
}