// Copyright (c) BandLab Technologies. All rights reserved.

using System;

namespace Cocktails.Identity.ViewModels
{
    public class TwitterUser : SocialUserBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTimeOffset? Birthday { get; set; }

        public string PictureUrl { get; set; }
    }
}
