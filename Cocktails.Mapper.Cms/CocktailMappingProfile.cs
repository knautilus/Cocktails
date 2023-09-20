using AutoMapper;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.Cocktails;

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