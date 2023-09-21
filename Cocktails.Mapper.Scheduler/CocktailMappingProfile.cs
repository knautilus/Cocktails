using AutoMapper;
using Cocktails.Entities.Elasticsearch;
using Cocktails.Entities.Sql;

namespace Cocktails.Mapper.Scheduler
{
    public class CocktailMappingProfile : Profile
    {
        public CocktailMappingProfile()
        {
            CreateMap<Cocktail, CocktailDocument>(MemberList.None);
            CreateMap<Flavor, FlavorDocument>(MemberList.None);
            CreateMap<CocktailCategory, CocktailCategoryDocument>(MemberList.None);
            CreateMap<Mix, MixDocument>(MemberList.None);
            CreateMap<Ingredient, IngredientDocument>(MemberList.None);
            CreateMap<MeasureUnit, MeasureUnitDocument>(MemberList.None);
        }
    }
}