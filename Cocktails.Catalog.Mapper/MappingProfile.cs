using AutoMapper;
using Cocktails.Data.Entities;
using Cocktails.Responses;

namespace Cocktails.Catalog.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CocktailCategory, CategoryModel>();
            CreateMap<CategoryModel, CocktailCategory>()
                //.IgnoreReadOnly()
                .ForMember(x => x.Cocktails, opt => opt.Ignore());

            CreateMap<Flavor, FlavorModel>();
            CreateMap<FlavorModel, Flavor>()
                //.IgnoreReadOnly()
                .ForMember(x => x.Cocktails, opt => opt.Ignore());

            CreateMap<Ingredient, IngredientModel>();
            CreateMap<IngredientModel, Ingredient>()
                //.IgnoreReadOnly()
                .ForMember(x => x.Mixes, opt => opt.Ignore());

            CreateMap<Mix, MixModel>();
            CreateMap<MixModel, Mix>()
                //.IgnoreReadOnly()
                .ForMember(x => x.Cocktail, opt => opt.Ignore());

            CreateMap<Cocktail, CocktailModel>();
            CreateMap<CocktailModel, Cocktail>();
                //.IgnoreReadOnly();
        }
    }
}
