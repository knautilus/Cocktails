using Cocktails.Data.Contexts;
using Cocktails.Data.Entities;
using Cocktails.Data.EFCore.Extensions;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.GraphQL.Catalog.Queries
{
    [ExtendObjectType("rootQuery")]
    public class IngredientQuery
    {
        public IQueryable<Ingredient> GetIngredients(string name, [Service(ServiceKind.Synchronized)] CocktailsContext dbContext)
        {
            return dbContext.Set<Ingredient>().ConditionalWhere(!string.IsNullOrWhiteSpace(name), x => EF.Functions.Like(x.Name, name.ToLikePattern()));
        }

        public Ingredient GetIngredient(long id, [Service(ServiceKind.Synchronized)] CocktailsContext dbContext)
        {
            return dbContext.Set<Ingredient>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
