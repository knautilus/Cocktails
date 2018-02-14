using System.Runtime.Serialization;

namespace Cocktails.Mailing.Mailgun.Data
{
    [DataContract]
    public class UnsubscribedMember
    {
        [DataMember(Name = "member")]
        public Member Member { get; set; }
    }
}
