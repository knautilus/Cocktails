using System.Threading;
using System.Threading.Tasks;
using Cocktails.Data.Domain;

namespace Cocktails.Data.Identity
{
    public interface IUserStorage<TKey> : IRepository<User>
        where TKey : struct
    {
        Task<User> GetById(TKey id, CancellationToken ct);
        Task<User> GetByName(string normalizedUserName, CancellationToken ct);
    }
}
