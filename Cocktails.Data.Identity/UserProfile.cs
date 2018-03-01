using System;

namespace Cocktails.Data.Identity
{
    public class UserProfile : BaseContentEntity<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public Gender? Gender { get; set; }
        public DateTimeOffset? Birthday { get; set; }
        public string PictureUrl { get; set; }
        public DateTimeOffset? PictureModifiedDate { get; set; }
    }
}
