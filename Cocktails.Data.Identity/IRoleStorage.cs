using System.Threading;
using System.Threading.Tasks;

namespace Cocktails.Data.Identity
{
    public interface IRoleStorage<TKey> : IRepository<Role>
        where TKey : struct
    {
        Task<Role> GetById(TKey id, CancellationToken ct);
        Task<Role> GetByName(string normalizedRoleName, CancellationToken ct);
    }
}
