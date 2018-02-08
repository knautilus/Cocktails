// Copyright (c) BandLab Technologies. All rights reserved.

using Newtonsoft.Json;

namespace Cocktails.Identity.ViewModels
{
    public class GoogleUser : SocialUserBase
    {
        [JsonProperty("sub")]
        public new string Id { get; set; }

        [JsonProperty("aud")]
        public string TokenInfo { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("picture")]
        public string PictureUrl { get; set; }
    }
}
