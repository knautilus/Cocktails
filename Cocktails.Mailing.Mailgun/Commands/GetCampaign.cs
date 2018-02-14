using System.Net;
using Newtonsoft.Json;
using RestSharp;
using Cocktails.Mailing.Mailgun.Data;

namespace Cocktails.Mailing.Mailgun.Commands
{
    public sealed class GetCampaign : ICommand<Campaign>
    {
        private const int MaxParameterValueLength = 64;
        private readonly string _id;

        public GetCampaign(string id)
        {
            CommandHelper.CheckNotNullOrEmpty(id, nameof(id));
            CommandHelper.CheckLessThan(id, MaxParameterValueLength, nameof(id));
            CommandHelper.CheckPureAscii(id, nameof(id));

            _id = id;
        }

        public Campaign CreateResult(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Campaign>(response.Content);
            }

            return null;
        }

        public IRestRequest GetRequest(string domain)
        {
            var request = new RestRequest
            {
                Resource = "{domain}/campaigns/{id}",
                Method = Method.GET
            };

            request.AddParameter("domain", domain, ParameterType.UrlSegment);
            request.AddParameter("id", _id, ParameterType.UrlSegment);

            return request;
        }

        public bool ResponseIsAdmissible(IRestResponse response)
        {
            return response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NotFound;
        }
    }
}
