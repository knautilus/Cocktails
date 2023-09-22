using System.Threading;
using System.Threading.Tasks;
using Cocktails.Models.Common;

namespace Cocktails.Cqrs.Mediator.Commands
{
    /// <summary>
    /// Defines a handler for a request
    /// </summary>
    /// <typeparam name="TCommand">The type of request being handled</typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand
    {
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<TResult> Handle(TCommand request, CancellationToken cancellationToken);
    }
}
