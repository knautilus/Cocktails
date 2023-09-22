using System.Threading;
using System.Threading.Tasks;
using Cocktails.Models.Common;

namespace Cocktails.Cqrs.Mediator.Queries
{
    /// <summary>
    /// Defines a handler for a request
    /// </summary>
    /// <typeparam name="TQuery">The type of request being handled</typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery
    {
        /// <summary>
        /// Handles a query
        /// </summary>
        /// <param name="query">The query</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
