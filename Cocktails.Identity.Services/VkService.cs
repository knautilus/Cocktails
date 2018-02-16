using System;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Common.Exceptions;
using Cocktails.Identity.ViewModels;
using RestSharp;

namespace Cocktails.Identity.Services
{
    public class VkService : ISocialService<VkUser>
    {
        private const string FacebookBaseUrl = "https://api.vk.com/method";
        private const string FacebookDataUrl = "account.getProfileInfo&access_token={0}";

        public async Task<VkUser> GetProfileAsync(string accessToken, CancellationToken cancellationToken)
        {
            var requestUrl = string.Format(FacebookDataUrl, accessToken);

            var client = new RestClient(FacebookBaseUrl);
            var request = new RestRequest(requestUrl, Method.GET);

            var response = await client.ExecuteTaskAsync<VkUser>(request, cancellationToken);
            if (!response.IsSuccessful)
            {
                throw new BadRequestException("Invalid oauth token");
            }
            var user = response.Data;

            //user.PictureUrl = user.Picture.Data.Url;

            return user;
        }
    }
}
