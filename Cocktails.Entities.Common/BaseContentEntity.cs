namespace Cocktails.Entities.Common
{
    public abstract class BaseContentEntity<TKey> : BaseEntity<TKey>
    {
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset ModifyDate { get; set; }
        public long CreateUserId { get; set; }
        public long ModifyUserId { get; set; }
    }
}
