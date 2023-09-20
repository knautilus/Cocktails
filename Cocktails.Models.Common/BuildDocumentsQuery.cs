namespace Cocktails.Models.Common
{
    public class BuildDocumentsQuery<TKey, TResponse> : GetManyQuery<int, TResponse>
    {
        public TKey[] Ids { get; set; }
    }
}
