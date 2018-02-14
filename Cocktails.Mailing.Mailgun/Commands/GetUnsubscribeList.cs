using System.Net;
using Cocktails.Mailing.Mailgun.Data;
using Newtonsoft.Json;
using RestSharp;

namespace Cocktails.Mailing.Mailgun.Commands
{
    /// <summary>
    /// Get the list of unsubscribed users.
    /// </summary>
    public class GetUnsubscribeList : ICommand<Unsubscribes>
    {
        private readonly int _limit;
        private readonly int _skip;
        private readonly string _mailingList;

        public GetUnsubscribeList(string mailingList, int limit = 100, int skip = 0)
        {
            CommandHelper.CheckNotNullOrEmpty(mailingList, nameof(mailingList));

            _mailingList = mailingList;
            _limit = limit;
            _skip = skip;
        }

        public Unsubscribes CreateResult(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Unsubscribes>(response.Content);
            }

            return null;
        }

        public IRestRequest GetRequest(string domain)
        {
            var request = new RestRequest
            {
                Resource = "lists/{address}/members",
                Method = Method.GET
            };

            request.AddParameter("address", _mailingList, ParameterType.UrlSegment);
            request.AddParameter("subscribed", "no");
            request.AddParameter("limit", _limit);
            request.AddParameter("skip", _skip);

            return request;
        }
    }
}
