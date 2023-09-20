namespace Cocktails.Entities.Elasticsearch.Helpers
{
    public interface IIndexConfiguration
    {
        string GetIndexName<T>();
    }
}
