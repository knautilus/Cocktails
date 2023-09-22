using System.Threading;
using System.Threading.Tasks;
using Cocktails.Models.Common;

namespace Cocktails.Cqrs.Mediator.Queries
{
    public interface IQueryProcessor
    {
        Task<TResult> Process<TResult>(
            IQuery query,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
