using Cocktails.Data.Contexts;
using Cocktails.Data.EFCore.Extensions;
using Cocktails.Data.Entities;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class CategoryQuery
    {
        public IQueryable<CocktailCategory> GetCategories(string name, CocktailsContext dbContext)
        {
            return dbContext.Set<CocktailCategory>().ConditionalWhere(!string.IsNullOrWhiteSpace(name), x => EF.Functions.Like(x.Name, name.ToLikePattern()));
        }

        public CocktailCategory GetCategory(long id, CocktailsContext dbContext)
        {
            return dbContext.Set<CocktailCategory>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
