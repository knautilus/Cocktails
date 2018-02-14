using RestSharp;

namespace Cocktails.Mailing.Mailgun.Commands
{
    /// <summary>
    /// Subscribe member to the mailing list.
    /// </summary>
    public class Subscribe : ICommand
    {
        private readonly string _address;
        private readonly string _mailingList;

        public Subscribe(string address, string mailingList)
        {
            CommandHelper.CheckNotNullOrEmpty(address, nameof(address));
            CommandHelper.CheckNotNullOrEmpty(mailingList, nameof(mailingList));

            _address = address;
            _mailingList = mailingList;
        }

        public IRestRequest GetRequest(string domain)
        {
            var request = new RestRequest
            {
                Resource = "lists/{address}/members/{member_address}",
                Method = Method.PUT
            };

            request.AddParameter("address", _mailingList, ParameterType.UrlSegment);
            request.AddParameter("member_address", _address, ParameterType.UrlSegment);
            request.AddParameter("subscribed", "yes");
            request.AddParameter("upsert", "yes");

            return request;
        }
    }
}
