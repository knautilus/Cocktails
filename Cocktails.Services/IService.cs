using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cocktails.Services
{
    public interface IService<TModel>
        where TModel : class
    {
        Task<TModel> ReadAsync(Guid key, CancellationToken cancellationToken);
        Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken);
    }
}
