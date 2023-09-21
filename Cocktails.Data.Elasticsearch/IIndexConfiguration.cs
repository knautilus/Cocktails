namespace Cocktails.Data.Elasticsearch
{
    public interface IIndexConfiguration
    {
        string GetIndexName<T>();
    }
}
