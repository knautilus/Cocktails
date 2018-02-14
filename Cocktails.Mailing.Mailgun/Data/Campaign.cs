using System.Runtime.Serialization;

namespace Cocktails.Mailing.Mailgun.Data
{
    [DataContract]
    public class Campaign
    {
        [DataMember(Name = "clicked_count")]
        public int ClickedCount { get; set; }

        [DataMember(Name = "opened_count")]
        public int OpenedCount { get; set; }

        [DataMember(Name = "submitted_count")]
        public int SubmittedCount { get; set; }

        [DataMember(Name = "unsubscribed_count")]
        public int UnsubscribedCount { get; set; }

        [DataMember(Name = "bounced_count")]
        public int BouncedCount { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "delivered_count")]
        public int DeliveredCount { get; set; }

        [DataMember(Name = "complained_count")]
        public int ComplainedCount { get; set; }

        [DataMember(Name = "dropped_count")]
        public int DroppedCount { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
