namespace Cocktails.Data.Core
{
    public interface IPagingQuery
    {
        int First { get; set; }
        int Offset { get; set; }
    }
}
