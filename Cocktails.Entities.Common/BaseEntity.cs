namespace Cocktails.Entities.Common
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
