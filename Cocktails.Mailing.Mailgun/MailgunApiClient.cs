using System;
using System.Net;
using System.Threading.Tasks;
using Cocktails.Mailing.Mailgun.Commands;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;

namespace Cocktails.Mailing.Mailgun
{
    public sealed class MailgunApiClient : IMailgunApiClient
    {
        private const string MailgunBaseUrl = "https://api.mailgun.net/v2";
        private readonly string _apiKey;
        private readonly string _domain;
        private readonly int _timeout;

        public MailgunApiClient(IOptions<MailingSettings> settings)
        {
            _apiKey = settings.Value.ApiKey;
            _domain = settings.Value.Domain;
            _timeout = 10000;
        }

        public void Execute(ICommand command)
        {
            try
            {
                var request = GetRequest(command);
                var response = CreateRestClient().Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                    return;

                if (response.StatusCode == HttpStatusCode.NotFound)
                    throw new MailgunNotFoundException(string.Format("Mailgun response: {0}", response.Content));

                throw new MailgunException(
                    string.Format(
                    "Mailgun invalid request: {0}. Status Code: {1}",
                    response.Content,
                    response.StatusCode));
            }
            catch (MailgunException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new MailgunException(e.Message, e);
            }
        }

        public Task<string> ExecuteAsync(ICommand command)
        {
            var tcs = new TaskCompletionSource<string>();

            try
            {
                var request = GetRequest(command);
                CreateRestClient().ExecuteAsync(
                    request,
                    response =>
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            tcs.SetResult(response.Content);
                        }
                        else if (response.ResponseStatus == ResponseStatus.TimedOut)
                        {
                            tcs.SetException(new MailgunException(string.Format("Mailgun invalid request: TimedOut")));
                        }
                        else
                        {
                            tcs.SetException(new MailgunException(string.Format("Mailgun invalid request: {0}", response.Content)));
                        }
                    });
            }
            catch (MailgunException ex)
            {
                tcs.SetException(ex);
            }
            catch (Exception ex)
            {
                tcs.SetException(new MailgunException(ex.Message, ex));
            }

            return tcs.Task;
        }

        public TResult Execute<TResult>(ICommand<TResult> command) where TResult : new()
        {
            try
            {
                var request = GetRequest(command);
                var response = CreateRestClient().Execute(request);
                return command.CreateResult(response);
            }
            catch (MailgunException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new MailgunException(e.Message, e);
            }
        }

        public Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command) where TResult : new()
        {
            var tcs = new TaskCompletionSource<TResult>();

            try
            {
                var request = GetRequest(command);
                CreateRestClient().ExecuteAsync(
                    request,
                    response =>
                    {
                        var result = command.CreateResult(response);
                        tcs.SetResult(result);
                    });
            }
            catch (MailgunException ex)
            {
                tcs.SetException(ex);
            }
            catch (Exception ex)
            {
                tcs.SetException(new MailgunException(ex.Message, ex));
            }

            return tcs.Task;
        }

        private IRestRequest GetRequest(ICommand command)
        {
            var request = command.GetRequest(_domain);
            request.Timeout = _timeout;

            return request;
        }

        private RestClient CreateRestClient()
        {
            return new RestClient
            {
                BaseUrl = new Uri(MailgunBaseUrl),
                Authenticator = new HttpBasicAuthenticator("api", _apiKey)
            };
        }
    }
}
