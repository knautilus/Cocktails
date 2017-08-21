using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cocktails.Services
{
    public interface IService<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<TModel>> GetAsync(CancellationToken cancellationToken);
        Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken);
        Task<TModel> UpdateAsync(Guid id, TModel model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
