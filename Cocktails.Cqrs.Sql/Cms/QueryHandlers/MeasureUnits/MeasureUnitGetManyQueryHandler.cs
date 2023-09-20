using Cocktails.Data.EFCore.Extensions;
using Cocktails.Entities.Sql;
using Cocktails.Models.Cms.Requests.MeasureUnits;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.QueryHandlers
{
    public class MeasureUnitGetManyQueryHandler : IRequestHandler<MeasureUnitGetManyQuery, IQueryable<MeasureUnit>>
    {
        private readonly DbContext _dbContext;

        public MeasureUnitGetManyQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IQueryable<MeasureUnit>> Handle(MeasureUnitGetManyQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Set<MeasureUnit>()
                .ConditionalWhere(!string.IsNullOrWhiteSpace(request.Name), x => EF.Functions.Like(x.Name, request.Name.ToLikePattern())));
        }
    }
}
