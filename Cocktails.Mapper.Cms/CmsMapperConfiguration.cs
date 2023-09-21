﻿using AutoMapper;
using Cocktails.Mapper.Common;

namespace Cocktails.Mapper.Cms
{
    public class CmsMapperConfiguration : IMapperProfileConfiguration
    {
        private readonly Profile[] _profiles = {
            new CocktailMappingProfile()
        };

        public Profile[] GetProfiles()
        {
            return _profiles;
        }
    }
}
