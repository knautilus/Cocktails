using AutoMapper;

using Cocktails.Data.Domain;
using Cocktails.Identity.ViewModels;

namespace Cocktails.Identity.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterModel, User>(MemberList.None)
                .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.Username));
        }
    }
}
