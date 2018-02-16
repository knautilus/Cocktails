using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Data.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Data.Identity.EFCore.Repositories
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
