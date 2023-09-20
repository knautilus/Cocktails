using Cocktails.Data.Core;
using MediatR;

namespace Cocktails.Models.Common
{
    public class GetManyQuery<TSortFieldEnum, TResponse> : ISortingQuery<TSortFieldEnum>, IRequest<TResponse>
        where TSortFieldEnum : struct
    {
        public int First { get; set; } = 10;
        public int Offset { get; set; } = 0;
        public TSortFieldEnum SortField { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.Desc;
    }
}
