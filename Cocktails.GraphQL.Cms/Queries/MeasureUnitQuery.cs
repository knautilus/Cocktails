using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests;
using Cocktails.Models.Cms.Requests.MeasureUnits;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class MeasureUnitQuery
    {
        public async Task<IQueryable<MeasureUnit>> GetMeasureUnits(MeasureUnitGetManyQuery? request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request ?? new MeasureUnitGetManyQuery(), cancellationToken);
        }

        public async Task<MeasureUnit> GetMeasureUnit(GetByIdQuery<long, MeasureUnit> request, [Service] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
    }
}
