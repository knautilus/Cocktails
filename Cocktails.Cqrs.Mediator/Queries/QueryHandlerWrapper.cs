using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Models.Common;

namespace Cocktails.Cqrs.Mediator.Queries
{
    internal abstract class QueryHandlerWrapper<TResult>
    {
        public abstract Task<TResult> Handle(IQuery request, CancellationToken cancellationToken, ServiceFactory serviceFactory);
    }

    internal class QueryHandlerWrapperImpl<TQuery, TResult> : QueryHandlerWrapper<TResult>
        where TQuery : IQuery
    {
        public override Task<TResult> Handle(IQuery request, CancellationToken cancellationToken, ServiceFactory serviceFactory)
        {
            var handler = serviceFactory
                .GetInstances<IQueryHandler<TQuery, TResult>>()
                .FirstOrDefault();

            // Регистрация хэндлера в CqrsRegistrationExtension.cs
            if (handler == null)
                throw new Exception($"Handler needs to be registered: {typeof(TQuery)}, {typeof(TResult)}. Request: {request.GetType()}");

            return handler.Handle((TQuery)request, cancellationToken);
        }
    }
}
