using AutoMapper;
using Cocktails.Data.Entities;
using Cocktails.Models.Cms.Requests.Cocktail;

namespace Cocktails.Mapper.Cms
{
    public class CocktailMappingProfile : Profile
    {
        public CocktailMappingProfile()
        {
            CreateMap<CocktailCreateCommand, Cocktail>(MemberList.None);
            CreateMap<CocktailUpdateCommand, Cocktail>(MemberList.None)
                .ForMember(x => x.Mixes, opt => opt.Ignore());
            CreateMap<MixInput, Mix>(MemberList.None);
            CreateMap<Mix, Mix>(MemberList.None);
        }
    }
}