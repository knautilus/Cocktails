using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Cocktails.Identity.ViewModels
{
    public class GooglePlusUser : SocialUserBase
    {
        private ICollection<Email> _emails;

        [JsonProperty("displayName")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birthday")]
        [DataType(DataType.Date)]
        public DateTimeOffset? Birthday { get; set; }

        [JsonProperty("emails")]
        public ICollection<Email> Emails
        {
            get => _emails ?? (_emails = new List<Email>());
            set => _emails = value;
        }

        [JsonProperty("image")]
        public Image Image { get; set; }

        public string PictureUrl { get; set; }
    }

    public class Email
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Image
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; }
    }
}
