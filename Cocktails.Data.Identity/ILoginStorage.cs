using Cocktails.Data.Domain;

namespace Cocktails.Data.Identity
{
    public interface ILoginStorage : IRepository<UserLogin>
    {
    }
}
