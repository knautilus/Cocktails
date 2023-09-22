using Cocktails.Data.Core;

namespace Cocktails.Models.Common
{
    public class GetManyQuery<TSortFieldEnum> : ISortingQuery<TSortFieldEnum>, IPagingQuery, IQuery
        where TSortFieldEnum : struct
    {
        public int Take { get; set; } = 10;
        public int Skip { get; set; } = 0;
        public TSortFieldEnum SortField { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.Desc;
    }
}
