using AutoMapper;
using Cocktails.Mapper.Cms;
using Cocktails.Mapper.Common;

namespace Cocktails.Catalog.Mapper
{
    public class CmsMapperConfiguration : IMapperProfileConfiguration
    {
        private readonly Profile[] _profiles;

        public CmsMapperConfiguration()
        {
            _profiles = new Profile[] {
                new CocktailMappingProfile()
            };
        }

        public Profile[] GetProfiles()
        {
            return _profiles;
        }
    }
}
