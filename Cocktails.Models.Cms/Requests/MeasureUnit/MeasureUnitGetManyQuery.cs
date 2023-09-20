using Cocktails.Entities.Sql;
using MediatR;

namespace Cocktails.Models.Cms.Requests.MeasureUnits
{
    public class MeasureUnitGetManyQuery : IRequest<IQueryable<MeasureUnit>>
    {
        public string Name { get; set; }
    }
}
