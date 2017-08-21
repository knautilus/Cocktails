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
            CreateMap<CategoryModel, Category>();

            CreateMap<Flavor, FlavorModel>();
            CreateMap<FlavorModel, Flavor>();

            CreateMap<Ingredient, IngredientModel>();
            CreateMap<IngredientModel, Ingredient>();

            CreateMap<Mix, MixModel>();
            CreateMap<MixModel, Mix>();

            CreateMap<Cocktail, CocktailModel>();
            CreateMap<CocktailModel, Cocktail>();
        }
    }
}
