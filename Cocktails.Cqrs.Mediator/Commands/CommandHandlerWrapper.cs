using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Models.Common;

namespace Cocktails.Cqrs.Mediator.Commands
{
    internal abstract class CommandHandlerWrapper<TResult>
    {
        public abstract Task<TResult> Handle(ICommand request, CancellationToken cancellationToken, ServiceFactory serviceFactory, Func<IEnumerable<Func<Task<TResult>>>, Task<TResult>> publish);
    }

    internal class CommandHandlerWrapperImpl<TCommand, TResult> : CommandHandlerWrapper<TResult>
        where TCommand : ICommand
    {
        public override Task<TResult> Handle(ICommand request, CancellationToken cancellationToken, ServiceFactory serviceFactory, Func<IEnumerable<Func<Task<TResult>>>, Task<TResult>> publish)
        {
            var handlers = serviceFactory
                .GetInstances<ICommandHandler<TCommand, TResult>>()
                .ToArray();

            if (!handlers.Any())
                throw new Exception($"Handler needs to be registered: {typeof(TCommand)}, {typeof(TResult)}. Request: {request.GetType()}");

            var functions = handlers
                .Select(x => new Func<Task<TResult>>(() => x.Handle((TCommand)request, cancellationToken)));

            return publish(functions);
        }
    }
}
