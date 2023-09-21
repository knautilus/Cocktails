using Cocktails.Entities.Sql;
using MediatR;

namespace Cocktails.Models.Scheduler.Requests.Mixes
{
    public class MixesGetByCocktailIdsQuery : IRequest<Mix[]>
    {
        public long[] CocktailIds { get; set; }
    }
}
