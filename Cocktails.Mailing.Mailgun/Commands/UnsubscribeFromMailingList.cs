using RestSharp;

namespace Cocktails.Mailing.Mailgun.Commands
{
    /// <summary>
    /// Unsubscribe member from the mailing list.
    /// </summary>
    public class UnsubscribeFromMailingList : ICommand
    {
        private readonly string _address;
        private readonly string _mailingList;

        public UnsubscribeFromMailingList(string address, string mailingList)
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
            request.AddParameter("subscribed", "no");
            request.AddParameter("upsert", "yes");

            return request;
        }
    }
}
