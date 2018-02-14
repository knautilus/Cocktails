using System;
using System.Runtime.Serialization;

namespace Cocktails.Mailing.Mailgun
{
    [Serializable]
    public class MailgunException : Exception
    {
        public MailgunException()
        {
        }

        public MailgunException(string message)
            : base(message)
        {
        }

        public MailgunException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected MailgunException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
