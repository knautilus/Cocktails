using System;

namespace Cocktails.Catalog.ViewModels
{
    public abstract class BaseModel
    {
        public virtual Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
