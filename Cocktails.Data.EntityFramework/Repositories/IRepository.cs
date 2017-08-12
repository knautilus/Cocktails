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
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token);
        Task<T> GetAsync(Func<IQueryable<T>, IQueryable<T>> query, CancellationToken token);
        Task<T> InsertAsync(T entity, CancellationToken token);
        Task UpdateAsync(T entity, CancellationToken token);
        //void Delete(T entity);
    }
}
