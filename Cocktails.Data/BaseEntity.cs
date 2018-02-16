using System;

namespace Cocktails.Data
{
    public abstract class BaseEntity<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
}
