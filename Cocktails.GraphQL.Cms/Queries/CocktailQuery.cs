using Cocktails.Data.Contexts;
using Cocktails.Data.EFCore.Extensions;
using Cocktails.Data.Entities;
using Cocktails.Models.Cms.Requests.Cocktail;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class CocktailQuery
    {
        public IQueryable<Cocktail> GetCocktails(CocktailGetManyQuery request, CocktailsContext dbContext)
        {
            return dbContext.Set<Cocktail>()
                .ConditionalWhere(!string.IsNullOrWhiteSpace(request.Name), x => EF.Functions.Like(x.Name, request.Name.ToLikePattern()))
                .ConditionalWhere(request.IngredientId.HasValue, x => x.Mixes.Any(m => m.IngredientId == request.IngredientId.Value))
                .ConditionalWhere(request.CocktailCategoryId.HasValue, x => x.CocktailCategoryId == request.CocktailCategoryId.Value)
                .ConditionalWhere(request.FlavorId.HasValue, x => x.CocktailCategoryId == request.FlavorId.Value);
        }

        public Cocktail GetCocktail(long id, CocktailsContext dbContext)
        {
            return dbContext.Set<Cocktail>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
