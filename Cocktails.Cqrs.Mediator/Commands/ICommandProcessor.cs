using System.Threading;
using System.Threading.Tasks;
using Cocktails.Models.Common;

namespace Cocktails.Cqrs.Mediator.Commands
{
    public interface ICommandProcessor
    {
        Task<TResult> Process<TResult>(
            ICommand command,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
