using System.Threading.Tasks;
using Cocktails.Mailing.Mailgun.Commands;

namespace Cocktails.Mailing.Mailgun
{
    public interface IMailgunApiClient
    {
        void Execute(ICommand command);

        Task<string> ExecuteAsync(ICommand command);

        TResult Execute<TResult>(ICommand<TResult> command) where TResult : new();

        Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command) where TResult : new();
    }
}
