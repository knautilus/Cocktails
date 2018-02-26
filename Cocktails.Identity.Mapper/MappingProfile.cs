using System;
using AutoMapper;
using Cocktails.Common;
using Cocktails.Data.Identity;
using Cocktails.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;

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

            CreateMap<UserLoginInfo, SocialLoginModel>(MemberList.None)
                .ForMember(x => x.AccessToken, opt => opt.MapFrom(x => x.ProviderKey))
                .ForMember(x => x.LoginProvider, opt => opt.MapFrom(x => Enum.Parse<LoginProviderType>(x.LoginProvider)));
        }
    }
}
