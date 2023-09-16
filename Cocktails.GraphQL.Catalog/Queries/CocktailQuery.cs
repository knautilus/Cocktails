using Cocktails.Data.Contexts;
using Cocktails.Data.Entities;
using Cocktails.Data.EFCore.Extensions;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.GraphQL.Catalog.Queries
{
    [ExtendObjectType("rootQuery")]
    public class CocktailQuery
    {
        public IQueryable<Cocktail> GetCocktails(string name, [Service(ServiceKind.Synchronized)] CocktailsContext dbContext)
        {
            return dbContext.Set<Cocktail>().ConditionalWhere(!string.IsNullOrWhiteSpace(name), x => EF.Functions.Like(x.Name, name.ToLikePattern()));
        }

        public Cocktail GetCocktail(long id, [Service(ServiceKind.Synchronized)] CocktailsContext dbContext)
        {
            return dbContext.Set<Cocktail>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
