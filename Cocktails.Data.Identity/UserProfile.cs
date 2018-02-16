using System;

namespace Cocktails.Data.Identity
{
    public class UserProfile : BaseContentEntity<long>
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public Gender? Gender { get; set; }
        public DateTimeOffset? Birthday { get; set; }
    }
}
