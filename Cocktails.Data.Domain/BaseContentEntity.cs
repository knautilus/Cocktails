using System;

namespace Cocktails.Data.Domain
{
    public abstract class BaseContentEntity<TKey> : BaseEntity<TKey>, IContentEntity where TKey : struct
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
