using Cocktails.Data.Domain;
using Cocktails.Data.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocktails.Data.EFCore.Repositories
{
    public class UserStorage : Repository<User>, IUserStorage<long>
    {
        public UserStorage(DbContext context) : base(context)
        {
        }

        public Task<User> GetById(long id, CancellationToken ct)
        {
            return GetSingleAsync(x => x.Where(u => u.Id == id), ct);
        }

        public Task<User> GetByName(string normalizedUserName, CancellationToken ct)
        {
            return GetSingleAsync(x => x.Where(u => u.UserName == normalizedUserName), ct);
        }
    }
}
