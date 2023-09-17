using System.ComponentModel;

namespace Cocktails.Models.Common
{
    public class UpdateCommand<TKey> : BaseModel<TKey>
    {
        [ReadOnly(true)]
        public DateTimeOffset ModifyDate { get; set; }
        [ReadOnly(true)]
        public int ModifyUserId { get; set; }
    }
}
