using AutoMapper;
using Cocktails.Data.Domain;
using Cocktails.ViewModels;

namespace Cocktails.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>().IgnoreReadOnly();

            CreateMap<Flavor, FlavorModel>();
            CreateMap<FlavorModel, Flavor>().IgnoreReadOnly();

            CreateMap<Ingredient, IngredientModel>();
            CreateMap<IngredientModel, Ingredient>().IgnoreReadOnly();

            CreateMap<Mix, MixModel>();
            CreateMap<MixModel, Mix>().IgnoreReadOnly();

            CreateMap<Cocktail, CocktailModel>();
            CreateMap<CocktailModel, Cocktail>().IgnoreReadOnly();
        }
    }
}
