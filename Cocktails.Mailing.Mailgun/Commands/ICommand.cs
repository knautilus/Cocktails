using RestSharp;

namespace Cocktails.Mailing.Mailgun.Commands
{
    public interface ICommand
    {
        IRestRequest GetRequest(string domain);
    }

    public interface ICommand<out TResult> : ICommand where TResult : new()
    {
        TResult CreateResult(IRestResponse response);
    }
}
