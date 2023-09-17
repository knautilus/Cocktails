using Cocktails.Data.Contexts;
using Cocktails.Data.EFCore.Extensions;
using Cocktails.Data.Entities;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class IngredientQuery
    {
        public IQueryable<Ingredient> GetIngredients(string name, CocktailsContext dbContext)
        {
            return dbContext.Set<Ingredient>().ConditionalWhere(!string.IsNullOrWhiteSpace(name), x => EF.Functions.Like(x.Name, name.ToLikePattern()));
        }

        public Ingredient GetIngredient(long id, CocktailsContext dbContext)
        {
            return dbContext.Set<Ingredient>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
