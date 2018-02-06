using System;

namespace Cocktails.Data.Domain
{
    public abstract class BaseEntity<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
}
