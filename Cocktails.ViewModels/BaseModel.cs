using System;
using System.ComponentModel;

namespace Cocktails.ViewModels
{
    public abstract class BaseModel
    {
        [ReadOnly(true)]
        public virtual Guid Id { get; set; }
        [ReadOnly(true)]
        public DateTimeOffset CreatedDate { get; set; }
        [ReadOnly(true)]
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
