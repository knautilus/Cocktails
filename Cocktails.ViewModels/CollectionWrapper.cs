using System.Collections.Generic;

namespace Cocktails.ViewModels
{
    public class CollectionWrapper<T>
    {
        public IEnumerable<T> Data { get; set; }
        public PagingModel Paging { get; set; }
    }
}
