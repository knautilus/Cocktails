using Cocktails.Data.Contexts;
using Cocktails.Data.Entities;
using Cocktails.Data.EFCore.Extensions;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.GraphQL.Catalog.Queries
{
    [ExtendObjectType("rootQuery")]
    public class FlavorQuery
    {
        public IQueryable<Flavor> GetFlavors(string name, [Service(ServiceKind.Synchronized)] CocktailsContext dbContext)
        {
            return dbContext.Set<Flavor>().ConditionalWhere(!string.IsNullOrWhiteSpace(name), x => EF.Functions.Like(x.Name, name.ToLikePattern()));
        }

        public Flavor GetFlavor(long id, [Service(ServiceKind.Synchronized)] CocktailsContext dbContext)
        {
            return dbContext.Set<Flavor>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
