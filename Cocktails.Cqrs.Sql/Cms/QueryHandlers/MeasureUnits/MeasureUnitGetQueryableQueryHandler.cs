using Cocktails.Data.EFCore.Extensions;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.MeasureUnits;
using Cocktails.Models.Cms.Requests.MeasureUnits;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.QueryHandlers.MeasureUnits
{
    public class MeasureUnitGetQueryableQueryHandler : GetQueryableQueryHandler<MeasureUnitGetQueryableQuery, MeasureUnit>
    {
        public MeasureUnitGetQueryableQueryHandler(DbContext dbContext) : base(dbContext)
        {
        }

        protected override Func<IQueryable<MeasureUnit>, MeasureUnitGetQueryableQuery, IQueryable<MeasureUnit>> Filter =>
            (source, request) => source
                .ConditionalWhere(!string.IsNullOrWhiteSpace(request.Name), x => EF.Functions.Like(x.Name, request.Name.ToLikePattern()));
    }
}
