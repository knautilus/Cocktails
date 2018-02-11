using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cocktails.Identity.ViewModels
{
    public class VkUser : SocialUserBase
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("sex")]
        public int Sex { get; set; }

        [JsonProperty("bdate")]
        [DataType(DataType.Date)]
        public DateTimeOffset? Birthday { get; set; }
    }
}
