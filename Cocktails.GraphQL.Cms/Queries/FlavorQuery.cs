using Cocktails.Data.Contexts;
using Cocktails.Data.EFCore.Extensions;
using Cocktails.Data.Entities;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class FlavorQuery
    {
        public IQueryable<Flavor> GetFlavors(string name, CocktailsContext dbContext)
        {
            return dbContext.Set<Flavor>().ConditionalWhere(!string.IsNullOrWhiteSpace(name), x => EF.Functions.Like(x.Name, name.ToLikePattern()));
        }

        public Flavor GetFlavor(long id, CocktailsContext dbContext)
        {
            return dbContext.Set<Flavor>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
