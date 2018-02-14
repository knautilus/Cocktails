using System.Collections.Generic;
using System.Linq;

namespace Cocktails.Mailing.Models
{
    public sealed class MailMessage
    {
        public MailMessage()
        {
            To = new MailAddressCollection();
            Attachments = new List<Attachment>();
            IsBodyHtml = true;
        }

        public MailMessage(string subject, string body, MailAddress from, params MailAddress[] to)
            : this()
        {
            Subject = subject;
            Body = body;
            From = from;
            To.AddRange(to);
        }

        public MailMessage(string subject, string body, string from, params string[] to)
            : this(subject, body, new MailAddress(from), to)
        {
        }

        public MailMessage(string subject, string body, string from, params MailAddress[] to)
            : this(subject, body, new MailAddress(from), to)
        {
        }

        public MailMessage(string subject, string body, MailAddress from, params string[] to)
            : this(subject, body, from, to.Select(address => new MailAddress(address)).ToArray())
        {
        }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsBodyHtml { get; set; }

        public MailAddress From { get; set; }

        public MailAddressCollection To { get; set; }

        public List<Attachment> Attachments { get; set; }

        public string Tag { get; set; }

        public string Campaign { get; set; }

        public override string ToString()
        {
            return string.Format("From:{0} , To:{1}, Subject:{2}", From, To, Subject);
        }
    }
}
