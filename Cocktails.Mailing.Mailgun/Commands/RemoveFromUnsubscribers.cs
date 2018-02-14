using RestSharp;

namespace Cocktails.Mailing.Mailgun.Commands
{
    public class RemoveFromUnsubscribers : ICommand
    {
        private readonly string _address;

        public RemoveFromUnsubscribers(string address)
        {
            CommandHelper.CheckNotNullOrEmpty(address, nameof(address));

            _address = address;
        }

        public IRestRequest GetRequest(string domain)
        {
            var request = new RestRequest
            {
                Resource = "{domain}/unsubscribes/{address}",
                Method = Method.DELETE
            };

            request.AddParameter("domain", domain, ParameterType.UrlSegment);
            request.AddParameter("address", _address, ParameterType.UrlSegment);

            return request;
        }
    }
}
