using System.Threading;
using System.Threading.Tasks;
using Cocktails.Identity.ViewModels;

namespace Cocktails.Identity.Services
{
    public interface ISocialService<T> where T : SocialUserBase
    {
        Task<T> GetProfileAsync(string accessToken, CancellationToken cancellationToken);
    }
}
