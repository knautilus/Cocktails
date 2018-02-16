using System;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Common.Models;

namespace Cocktails.Common.Services
{
    public interface IService<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<CollectionWrapper<TModel>> GetAllAsync(QueryContext context, CancellationToken cancellationToken);
        Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken);
        Task<TModel> UpdateAsync(Guid id, TModel model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
