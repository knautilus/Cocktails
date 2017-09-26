namespace Cocktails.Catalog.ViewModels
{
    public class CollectionWrapper<T>
    {
        public T[] Data { get; set; }
        public PagingModel Paging { get; set; }
    }
}
