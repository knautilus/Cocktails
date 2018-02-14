using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cocktails.Mailing.Mailgun.Data
{
    [DataContract]
    public class Unsubscribes
    {
        public Unsubscribes()
        {
            this.Items = new List<Member>();
        }

        [DataMember(Name = "total_count")]
        public int TotalCount { get; set; }

        [DataMember(Name = "items")]
        public IEnumerable<Member> Items { get; set; }
    }
}
