using System;
using System.Threading.Tasks;
using Cocktails.Mailing.Mailgun.Commands;
using Cocktails.Mailing.Mailgun.Data;
using Cocktails.Mailing.Models;

namespace Cocktails.Mailing.Mailgun
{
    public class MailgunSender : IMailgunSender
    {
        private readonly IMailgunApiClient _mailgunApiClient;

        public MailgunSender(IMailgunApiClient mailgunApiClient)
        {
            _mailgunApiClient = mailgunApiClient;
        }

        public void Send(MailMessage message)
        {
            _mailgunApiClient.Execute(new SendEmail(message));
        }

        public MailSendingResult SendWithResult(MailMessage mailMessage, string campaignId = null)
        {
            if (!string.IsNullOrEmpty(campaignId))
                mailMessage.Campaign = campaignId;

            try
            {
                _mailgunApiClient.Execute(new SendEmail(mailMessage));
                return new MailSendingResult {Status = MailSendingStatus.Success};
            }
            catch (Exception ex)
            {
                return new MailSendingResult {Status = MailSendingStatus.SendingFailed, Message = ex.Message};
            }
        }

        public Task<MailSendingResult> SendAsync(MailMessage mailMessage, string campaignId = null)
        {
            var tcs = new TaskCompletionSource<MailSendingResult>();
                if (!string.IsNullOrEmpty(campaignId))
                    mailMessage.Campaign = campaignId;
                try
                {
                    var task = _mailgunApiClient.ExecuteAsync(new SendEmail(mailMessage));
                    var newTask = task.ContinueWith(
                        taskResult =>
                        {
                            if (taskResult.IsFaulted)
                            {
                                var mailSendingResult = new MailSendingResult
                                {
                                    Status = MailSendingStatus.SendingFailed,
                                    Message = taskResult.Exception.Message
                                };
                                return mailSendingResult;
                            }
                            return new MailSendingResult
                            {
                                Status = MailSendingStatus.Success,
                                Message = taskResult.Result
                            };
                        });
                    return newTask;
                }
                catch (Exception ex)
                {
                    tcs.SetResult(new MailSendingResult { Status = MailSendingStatus.SendingFailed, Message = ex.Message });
                }

            return tcs.Task;
        }

        public Campaign GetCampaign(string campaignId)
        {
            if (!string.IsNullOrEmpty(campaignId))
            {
                try
                {
                    var campaign = _mailgunApiClient.Execute(new GetCampaign(campaignId));

                    if (campaign != null && !string.IsNullOrEmpty(campaign.Id) && !string.IsNullOrEmpty(campaign.Name))
                        return campaign;
                }
                catch (Exception)
                {
                }
            }

            return null;
        }

        public bool CreateCampaignIfNotExist(string campaignId, string campaignName)
        {
            if (!string.IsNullOrEmpty(campaignId) && !string.IsNullOrEmpty(campaignName))
            {
                var campaign = GetCampaign(campaignId);
                try
                {
                    if (campaign == null)
                    {
                        var result = _mailgunApiClient.Execute(new CreateCampaign(campaignId, campaignName));
                    }
                    return true;
                }
                catch (Exception)
                {
                }
            }
            return false;
        }
    }
}
