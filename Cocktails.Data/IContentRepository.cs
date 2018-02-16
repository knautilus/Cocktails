namespace Cocktails.Data
{
    public interface IContentRepository<TKey, TEntity> : IRepository<TEntity>
        where TKey: struct
        where TEntity : BaseContentEntity<TKey>
    { }
}
