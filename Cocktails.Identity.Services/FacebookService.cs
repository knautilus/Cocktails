using System;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Common.Exceptions;
using Cocktails.Identity.ViewModels;
using RestSharp;

namespace Cocktails.Identity.Services
{
    public class FacebookService : ISocialService<FacebookUser>
    {
        private const string FacebookBaseUrl = "https://graph.facebook.com";
        private const string FacebookDataUrl = "me?fields=id,name,email,gender,birthday,picture&access_token={0}";

        public async Task<FacebookUser> GetProfileAsync(string accessToken, CancellationToken cancellationToken)
        {
            var requestUrl = string.Format(FacebookDataUrl, accessToken);

            var client = new RestClient(FacebookBaseUrl);
            var request = new RestRequest(requestUrl, Method.GET);

            var response = await client.ExecuteTaskAsync<FacebookUser>(request, cancellationToken);
            if (!response.IsSuccessful)
            {
                throw new BadRequestException("Invalid oauth token");
            }
            var user = response.Data;

            user.PictureUrl = user.Picture.Data.Url;

            return user;
        }
    }
}
