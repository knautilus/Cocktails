namespace Cocktails.Mailing.Mailgun.Commands
{
    public class UnsubscribeFromDomain : UnsubscribeFromTag
    {
        public UnsubscribeFromDomain(string address)
            : base(address, "*")
        {
        }
    }
}
