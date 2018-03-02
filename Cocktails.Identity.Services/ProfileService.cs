using Cocktails.Common.Services;
using Cocktails.Data;
using Cocktails.Data.Identity;
using Cocktails.Identity.ViewModels;
using Cocktails.Mapper;

namespace Cocktails.Identity.Services
{
    public class UserProfileService : BaseService<long, UserProfile, UserProfileModel>
    {
        public UserProfileService(IContentRepository<long, UserProfile> repository, IModelMapper mapper)
            : base(repository, mapper) { }
    }
}
