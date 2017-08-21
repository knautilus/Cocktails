using System;

namespace Cocktails.ViewModels
{
    public abstract class BaseModel
    {
        public virtual Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
