using System.Net;
using Cocktails.Mailing.Mailgun.Data;
using RestSharp;

namespace Cocktails.Mailing.Mailgun.Commands
{
    public sealed class CreateCampaign : ICommand<CreateCampaignResult>
    {
        private const int MaxParameterValueLength = 64;
        private readonly string _id;
        private readonly string _name;

        public CreateCampaign(string id, string name)
        {
            CommandHelper.CheckNotNullOrEmpty(id, nameof(id));
            CommandHelper.CheckNotNullOrEmpty(name, nameof(name));
            CommandHelper.CheckLessThan(id, MaxParameterValueLength, nameof(id));
            CommandHelper.CheckLessThan(name, MaxParameterValueLength, nameof(name));
            CommandHelper.CheckPureAscii(id, nameof(id));

            _id = id;
            _name = name;
        }

        public CreateCampaignResult CreateResult(IRestResponse response)
        {
            var result = new CreateCampaignResult {Message = response.Content};

            if (response.StatusCode == HttpStatusCode.OK)
            {
                result.CreateCampaignResultType = CreateCampaignResultTypes.Created;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest && response.Content.Contains("Duplicated id") &&
                     response.Content.Contains("has the same"))
            {
                result.CreateCampaignResultType = CreateCampaignResultTypes.AlreadyExists;
            }
            else
            {
                result.CreateCampaignResultType = CreateCampaignResultTypes.CanNotBeCreated;
            }

            return result;
        }

        public IRestRequest GetRequest(string domain)
        {
            var request = new RestRequest
            {
                Resource = "{domain}/campaigns",
                Method = Method.POST
            };

            request.AddParameter("domain", domain, ParameterType.UrlSegment);
            request.AddParameter("id", _id);
            request.AddParameter("name", _name);

            return request;
        }
    }
}
