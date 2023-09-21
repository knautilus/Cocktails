using Cocktails.Data.Core;
using MediatR;

namespace Cocktails.Models.Common
{
    public class GetManyQuery<TEntity, TSortFieldEnum> : ISortingQuery<TSortFieldEnum>, IPagingQuery, IRequest<TEntity[]>, IRequest<int>
        where TSortFieldEnum : struct
    {
        public int Take { get; set; } = 10;
        public int Skip { get; set; } = 0;
        public TSortFieldEnum SortField { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.Desc;
    }
}
