namespace Cocktails.Data.Core
{
    public interface ISortingQuery<TSortFieldEnum>
        where TSortFieldEnum : struct
    {
        SortDirection SortDirection { get; set; }
        TSortFieldEnum SortField { get; set; }
    }
}