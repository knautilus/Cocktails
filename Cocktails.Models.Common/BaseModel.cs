using System.ComponentModel;

namespace Cocktails.Models.Common
{
    public abstract class BaseModel<TKey>
    {
        [ReadOnly(true)]
        public virtual TKey Id { get; set; }
    }
}