using Cocktails.Data.Domain;

namespace Cocktails.Data
{
    public interface IContentRepository<TEntity> : IRepository<TEntity> where TEntity : BaseContentEntity { }
}
