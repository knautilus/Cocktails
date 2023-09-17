using Cocktails.Data.Entities;
using MediatR;

namespace Cocktails.Models.Cms.Requests.Cocktails
{
    public class CocktailGetManyQuery : IRequest<IQueryable<Cocktail>>
    {
        public string Name { get; set; }
        public long? IngredientId { get; set; }
        public long? CocktailCategoryId { get; set; }
        public long? FlavorId { get; set; }
    }
}
