using Cocktails.Data.Contexts;
using Cocktails.Data.Entities;
using Cocktails.Data.EFCore.Extensions;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.GraphQL.Catalog.Queries
{
    [ExtendObjectType("rootQuery")]
    public class CategoryQuery
    {
        public IQueryable<CocktailCategory> GetCategories(string name, [Service(ServiceKind.Synchronized)] CocktailsContext dbContext)
        {
            return dbContext.Set<CocktailCategory>().ConditionalWhere(!string.IsNullOrWhiteSpace(name), x => EF.Functions.Like(x.Name, name.ToLikePattern()));
        }

        public CocktailCategory GetCategory(long id, [Service(ServiceKind.Synchronized)] CocktailsContext dbContext)
        {
            return dbContext.Set<CocktailCategory>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
