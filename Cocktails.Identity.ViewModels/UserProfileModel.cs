using System;
using Cocktails.Common.Models;

namespace Cocktails.Identity.ViewModels
{
    public class UserProfileModel : BaseModel<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public string City { get; set; }
        public Gender? Gender { get; set; }
        public DateTimeOffset? Birthday { get; set; }
        public string PictureUrl { get; set; }
        public DateTimeOffset? PictureModifiedDate { get; set; }
    }
}
