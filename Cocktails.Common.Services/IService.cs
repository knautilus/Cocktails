using System.Threading;
using System.Threading.Tasks;
using Cocktails.Common.Models;

namespace Cocktails.Common.Services
{
    public interface IService<TKey, TEntity, TModel>
        where TKey : struct
        where TEntity : class
        where TModel : class
    {
        Task<TModel> GetByIdAsync(TKey id, CancellationToken cancellationToken);
        Task<CollectionWrapper<TModel>> GetAllAsync(QueryContext context, CancellationToken cancellationToken);
        Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken);
        Task<TModel> UpdateAsync(TKey id, TModel model, CancellationToken cancellationToken);
        Task DeleteAsync(TKey id, CancellationToken cancellationToken);
    }
}
