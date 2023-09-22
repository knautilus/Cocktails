using Cocktails.Cqrs.Mediator.Queries;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.MeasureUnits;
using Cocktails.Models.Common;
using HotChocolate;
using HotChocolate.Types;

namespace Cocktails.GraphQL.Cms.Queries
{
    [ExtendObjectType("rootQuery")]
    public class MeasureUnitQuery
    {
        public async Task<IQueryable<MeasureUnit>> GetMeasureUnits(MeasureUnitGetQueryableQuery? request, [Service] IQueryProcessor queryProcessor, CancellationToken cancellationToken)
        {
            return await queryProcessor.Process<IQueryable<MeasureUnit>>(request ?? new MeasureUnitGetQueryableQuery(), cancellationToken);
        }

        public async Task<MeasureUnit> GetMeasureUnit(GetByIdQuery<long> request, [Service] IQueryProcessor queryProcessor, CancellationToken cancellationToken)
        {
            return await queryProcessor.Process<MeasureUnit>(request, cancellationToken);
        }
    }
}
