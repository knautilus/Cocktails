using AutoMapper;

using Cocktails.Catalog.ViewModels;
using Cocktails.Data.Catalog;
using Cocktails.Mapper;

namespace Cocktails.Catalog.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>()
                .IgnoreReadOnly()
                .ForMember(x => x.Ingredients, opt => opt.Ignore());

            CreateMap<Flavor, FlavorModel>();
            CreateMap<FlavorModel, Flavor>()
                .IgnoreReadOnly()
                .ForMember(x => x.Ingredients, opt => opt.Ignore());

            CreateMap<Ingredient, IngredientModel>();
            CreateMap<IngredientModel, Ingredient>()
                .IgnoreReadOnly()
                .ForMember(x => x.Mixes, opt => opt.Ignore());

            CreateMap<Mix, MixModel>();
            CreateMap<MixModel, Mix>()
                .IgnoreReadOnly()
                .ForMember(x => x.Cocktail, opt => opt.Ignore());

            CreateMap<Cocktail, CocktailModel>();
            CreateMap<CocktailModel, Cocktail>().IgnoreReadOnly();
        }
    }
}
