using System;

namespace Cocktails.Data.Domain
{
    public abstract class BaseContentEntity : BaseEntity
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
