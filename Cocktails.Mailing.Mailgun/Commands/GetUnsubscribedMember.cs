using System.Net;
using Cocktails.Mailing.Mailgun.Data;
using Newtonsoft.Json;
using RestSharp;

namespace Cocktails.Mailing.Mailgun.Commands
{
    /// <summary>
    /// Check if a given address is unsubscribed in the mailing list.
    /// </summary>
    public class GetUnsubscribedMember : ICommand<UnsubscribedMember>
    {
        private readonly string _address;
        private readonly string _mailingList;

        public GetUnsubscribedMember(string address, string mailingList)
        {
            CommandHelper.CheckNotNullOrEmpty(address, nameof(address));
            CommandHelper.CheckNotNullOrEmpty(mailingList, nameof(mailingList));

            _address = address;
            _mailingList = mailingList;
        }

        public UnsubscribedMember CreateResult(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<UnsubscribedMember>(response.Content);
            }

            return null;
        }

        public IRestRequest GetRequest(string domain)
        {
            var request = new RestRequest
            {
                Resource = "lists/{address}/members/{member_address}",
                Method = Method.GET
            };

            request.AddParameter("address", _mailingList, ParameterType.UrlSegment);
            request.AddParameter("member_address", _address, ParameterType.UrlSegment);

            return request;
        }
    }
}
