using System;
using System.ComponentModel;

namespace Cocktails.Common.Models
{
    public abstract class BaseModel<TKey>
        where TKey : struct
    {
        [ReadOnly(true)]
        public virtual TKey Id { get; set; }
        [ReadOnly(true)]
        public DateTimeOffset CreatedDate { get; set; }
        [ReadOnly(true)]
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
