using AutoMapper;
using Cocktails.Mapper.Common;

namespace Cocktails.Catalog.Mapper
{
    public class CatalogMapperConfiguration : IMapperProfileConfiguration
    {
        private readonly Profile[] _profiles;

        public CatalogMapperConfiguration()
        {
            _profiles = new Profile[] {
                new MappingProfile()
            };
        }

        public Profile[] GetProfiles()
        {
            return _profiles;
        }
    }
}
