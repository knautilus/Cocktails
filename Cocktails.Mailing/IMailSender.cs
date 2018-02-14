using Cocktails.Mailing.Models;

namespace Cocktails.Mailing
{
    public interface IMailSender
    {
        void Send(MailMessage message);
    }
}
