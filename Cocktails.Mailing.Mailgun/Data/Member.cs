using System.Runtime.Serialization;

namespace Cocktails.Mailing.Mailgun.Data
{
    [DataContract]
    public class Member
    {
        [DataMember(Name = "subscribed")]
        public bool Subscribed { get; set; }

        [DataMember(Name = "address")]
        public string Email { get; set; }
    }
}
