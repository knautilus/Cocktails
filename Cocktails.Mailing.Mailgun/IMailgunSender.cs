using System.Threading.Tasks;
using Cocktails.Mailing.Mailgun.Data;
using Cocktails.Mailing.Models;

namespace Cocktails.Mailing.Mailgun
{
    public interface IMailgunSender : IMailSender
    {
        Campaign GetCampaign(string campaignId);
        bool CreateCampaignIfNotExist(string campaignId, string campaignName);

        MailSendingResult SendWithResult(MailMessage mailMessage, string campaignId = null);
        Task<MailSendingResult> SendAsync(MailMessage mailMessage, string campaignId = null);
    }
}
