using Cocktails.Data.Entities;
using MediatR;

namespace Cocktails.Models.Cms.Requests.MeasureUnits
{
    public class MeasureUnitGetManyQuery : IRequest<IQueryable<MeasureUnit>>
    {
        public string Name { get; set; }
    }
}
