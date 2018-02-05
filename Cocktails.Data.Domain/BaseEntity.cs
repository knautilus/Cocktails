using System;

namespace Cocktails.Data.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
