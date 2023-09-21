using Cocktails.Entities.Sql;
using MediatR;

namespace Cocktails.Models.Cms.Requests.Mixes
{
    public class MixesGetByCocktailIdsQuery : IRequest<IQueryable<Mix>>
    {
        public long[] CocktailIds { get; set; }
    }
}
