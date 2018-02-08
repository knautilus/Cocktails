// Copyright (c) BandLab Technologies. All rights reserved.

using Newtonsoft.Json;

namespace Cocktails.Identity.ViewModels
{
    public class FacebookPicture
    {
        [JsonProperty("data")]
        public Image Data { get; set; }

        public class Image
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("is_silhouette")]
            public bool IsDefault { get; set; }
        }
    }
}
