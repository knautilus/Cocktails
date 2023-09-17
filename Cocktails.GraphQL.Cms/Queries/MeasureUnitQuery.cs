using Cocktails.Data.Contexts;
using Cocktails.Data.EFCore.Extensions;
using Cocktails.Data.Entities;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class MeasureUnitQuery
    {
        public IQueryable<MeasureUnit> GetMeasureUnits(string name, CocktailsContext dbContext)
        {
            return dbContext.Set<MeasureUnit>().ConditionalWhere(!string.IsNullOrWhiteSpace(name), x => EF.Functions.Like(x.Name, name.ToLikePattern()));
        }

        public MeasureUnit GetMeasureUnit(long id, CocktailsContext dbContext)
        {
            return dbContext.Set<MeasureUnit>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
