using RestSharp;

namespace Cocktails.Mailing.Mailgun.Commands
{
    /// <summary>
    /// Unsubscribe member from tag.
    /// </summary>
    public class UnsubscribeFromTag : ICommand
    {
        private readonly string _address;
        private readonly string _tag;

        public UnsubscribeFromTag(string address, string tag)
        {
            CommandHelper.CheckNotNullOrEmpty(address, nameof(address));
            CommandHelper.CheckNotNullOrEmpty(tag, nameof(tag));

            _address = address;
            _tag = tag;
        }

        public IRestRequest GetRequest(string domain)
        {
            var request = new RestRequest
            {
                Resource = "{domain}/unsubscribes",
                Method = Method.POST
            };

            request.AddParameter("domain", domain, ParameterType.UrlSegment);
            request.AddParameter("address", _address);
            request.AddParameter("tag", _tag);

            return request;
        }
    }
}
