using Cocktails.Data.Domain;
using Cocktails.Data.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocktails.Data.EFCore.Repositories
{
    public class RoleStorage : Repository<Role>, IRoleStorage
    {
        public RoleStorage(DbContext context) : base(context)
        {
        }

        public Task<Role> GetById(Guid id, CancellationToken ct)
        {
            return GetSingleAsync(x => x.Where(u => u.Id == id), ct);
        }

        public Task<Role> GetByName(string normalizedRoleName, CancellationToken ct)
        {
            return GetSingleAsync(x => x.Where(u => u.Name == normalizedRoleName), ct);
        }
    }
}
