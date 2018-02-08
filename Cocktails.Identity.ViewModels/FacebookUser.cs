// Copyright (c) BandLab Technologies. All rights reserved.

using System;

using Newtonsoft.Json;

namespace Cocktails.Identity.ViewModels
{
    public class FacebookUser : SocialUserBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birthday")]
        public DateTimeOffset? Birthday { get; set; }

        [JsonProperty("picture")]
        public FacebookPicture Picture { get; set; }

        public string PictureUrl { get; set; }
    }
}
