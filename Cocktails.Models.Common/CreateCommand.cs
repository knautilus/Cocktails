using System.ComponentModel;

namespace Cocktails.Models.Common
{
    public class CreateCommand<TKey> : UpdateCommand<TKey>
    {
        [ReadOnly(true)]
        public DateTimeOffset CreateDate { get; set; }
        [ReadOnly(true)]
        public int CreateUserId { get; set; }
    }
}
