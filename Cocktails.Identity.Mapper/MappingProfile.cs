using System;
using AutoMapper;
using Cocktails.Common;
using Cocktails.Data.Identity;
using Cocktails.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Gender = Cocktails.Identity.ViewModels.Gender;

namespace Cocktails.Identity.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterModel, User>(MemberList.None)
                .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.Username));

            CreateMap<FacebookUser, User>(MemberList.None)
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.MapFrom(y => UniqueNameGenerator.Generate("user")));

            CreateMap<GooglePlusUser, User>(MemberList.None)
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.MapFrom(y => UniqueNameGenerator.Generate("user")));

            CreateMap<VkUser, User>(MemberList.None)
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.MapFrom(y => UniqueNameGenerator.Generate("user")));

            CreateMap<UserLoginInfo, SocialLoginModel>(MemberList.None)
                .ForMember(x => x.AccessToken, opt => opt.MapFrom(x => x.ProviderKey))
                .ForMember(x => x.LoginProvider, opt => opt.MapFrom(x => Enum.Parse<LoginProviderType>(x.LoginProvider)));

            CreateMap<UserProfile, UserProfileModel>(MemberList.None)
                .ForMember(x => x.Gender, opt => opt.MapFrom(x => x.Gender.HasValue ? (Gender)(byte)x.Gender : (Gender?)null));
        }
    }
}
