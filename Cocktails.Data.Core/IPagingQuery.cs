namespace Cocktails.Data.Core
{
    public interface IPagingQuery
    {
        int Take { get; set; }
        int Skip { get; set; }
    }
}
