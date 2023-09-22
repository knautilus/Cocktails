using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Models.Common;

namespace Cocktails.Cqrs.Mediator.Commands
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly ServiceFactory _serviceFactory;
        //private readonly ApiMode _apiMode;

        public CommandProcessor(ServiceFactory serviceFactory
            //, ApiMode apiMode
            )
        {
            _serviceFactory = serviceFactory;
            //_apiMode = apiMode;
        }

        public Task<TResult> Process<TResult>(ICommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            //if (_apiMode.IsReadOnly)
            //{
            //    throw new Exception("The api is currently on read only mode");
            //}

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var commandType = command.GetType();
            var resultType = typeof(TResult);

            var handler = (CommandHandlerWrapper<TResult>)Activator.CreateInstance(typeof(CommandHandlerWrapperImpl<,>).MakeGenericType(commandType, resultType));

            return handler.Handle(command, cancellationToken, _serviceFactory, PublishCore);
        }

        protected virtual async Task<TResult> PublishCore<TResult>(IEnumerable<Func<Task<TResult>>> allHandlers)
        {
            var res = default(TResult);

            foreach (var handler in allHandlers)
            {
                res = await handler().ConfigureAwait(false);
            }
            return res;
        }
    }
}
