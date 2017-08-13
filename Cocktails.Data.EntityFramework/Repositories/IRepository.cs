using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Cocktails.Data.Domain;
using System.Linq;

namespace Cocktails.Data.EntityFramework.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetSingleAsync(Func<IQueryable<T>, IQueryable<T>> query, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> query, CancellationToken cancellationToken);
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        //void Delete(T entity);
    }
}
