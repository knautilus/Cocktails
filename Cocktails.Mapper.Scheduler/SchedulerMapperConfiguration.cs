using AutoMapper;
using Cocktails.Mapper.Common;

namespace Cocktails.Mapper.Scheduler
{
    public class SchedulerMapperConfiguration : IMapperProfileConfiguration
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
