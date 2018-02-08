// Copyright (c) BandLab Technologies. All rights reserved.

using Newtonsoft.Json;

namespace Cocktails.Identity.ViewModels
{
    public abstract class SocialUserBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
