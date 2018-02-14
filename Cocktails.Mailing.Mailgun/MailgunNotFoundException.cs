namespace Cocktails.Mailing.Mailgun
{
    public class MailgunNotFoundException : MailgunException
    {
        public MailgunNotFoundException(string message) : base(message)
        {
        }
    }
}
