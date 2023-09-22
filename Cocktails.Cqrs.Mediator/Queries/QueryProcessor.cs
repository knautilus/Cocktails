using System;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Models.Common;

namespace Cocktails.Cqrs.Mediator.Queries
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly ServiceFactory _serviceFactory;

        public QueryProcessor(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public virtual Task<TResult> Process<TResult>(IQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var queryType = query.GetType();
            var resultType = typeof(TResult);

            var handler =
                (QueryHandlerWrapper<TResult>) Activator.CreateInstance(
                    typeof(QueryHandlerWrapperImpl<,>).MakeGenericType(queryType, resultType));

            return handler.Handle(query, cancellationToken, _serviceFactory);
        }
    }
}
