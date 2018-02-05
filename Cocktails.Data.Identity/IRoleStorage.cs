using System;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Data.Domain;

namespace Cocktails.Data.Identity
{
    public interface IRoleStorage : IRepository<Role>
    {
        Task<Role> GetById(Guid id, CancellationToken ct);
        Task<Role> GetByName(string normalizedRoleName, CancellationToken ct);
    }
}
