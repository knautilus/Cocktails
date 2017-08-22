using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Cocktails.Data.Domain;
using System.Linq;

namespace Cocktails.Data.EntityFramework.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetSingleAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken cancellationToken);
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
