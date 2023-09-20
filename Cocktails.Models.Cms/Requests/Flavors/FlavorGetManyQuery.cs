using Cocktails.Entities.Sql;
using MediatR;

namespace Cocktails.Models.Cms.Requests.Flavors
{
    public class FlavorGetManyQuery : IRequest<IQueryable<Flavor>>
    {
        public string Name { get; set; }
    }
}
