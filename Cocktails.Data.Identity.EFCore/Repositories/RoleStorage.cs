using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Data.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Data.Identity.EFCore.Repositories
{
    public class RoleStorage : Repository<Role>, IRoleStorage<long>
    {
        public RoleStorage(DbContext context) : base(context)
        {
        }

        public Task<Role> GetById(long id, CancellationToken ct)
        {
            return GetSingleAsync(x => x.Where(u => u.Id == id), ct);
        }

        public Task<Role> GetByName(string normalizedRoleName, CancellationToken ct)
        {
            return GetSingleAsync(x => x.Where(u => u.Name == normalizedRoleName), ct);
        }
    }
}
