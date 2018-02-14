using RestSharp;
using Cocktails.Mailing.Models;

namespace Cocktails.Mailing.Mailgun.Commands
{
    public sealed class SendEmail : ICommand
    {
        private readonly MailMessage _message;

        public SendEmail(MailMessage message)
        {
            _message = message;
        }

        public IRestRequest GetRequest(string domain)
        {
            var request = new RestRequest
            {
                Resource = "{domain}/messages",
                Method = Method.POST
            };

            request.AddParameter("domain", domain, ParameterType.UrlSegment);
            request.AddParameter("subject", _message.Subject);

            var messageType = _message.IsBodyHtml ? "html" : "text";
            request.AddParameter(messageType, _message.Body);

            request.AddParameter("from", _message.From.ToString());
            foreach (var to in _message.To)
            {
                request.AddParameter("to", to.ToString());
            }

            if (!string.IsNullOrEmpty(_message.Tag))
            {
                request.AddParameter("o:tag", _message.Tag);
            }

            if (!string.IsNullOrEmpty(_message.Campaign))
            {
                request.AddParameter("o:campaign", _message.Campaign);
            }

            foreach (var attachment in _message.Attachments)
            {
                request.AddFile("attachment", attachment.Content, attachment.Name, attachment.MediaType);
            }

            return request;
        }
    }
}
