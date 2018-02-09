using System;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Common.Exceptions;
using Cocktails.Identity.ViewModels;
using RestSharp;

namespace Cocktails.Identity.Services
{
    public class GooglePlusService : ISocialService<GooglePlusUser>
    {
        private const string GooglePlusBaseUrl = "https://www.googleapis.com/plus/v1";
        private const string GooglePlusDataUrl = "people/me?access_token={0}";

        public async Task<GooglePlusUser> GetProfileAsync(string accessToken, CancellationToken cancellationToken)
        {
            var requestUrl = string.Format(GooglePlusDataUrl, accessToken);

            var client = new RestClient(GooglePlusBaseUrl);
            var request = new RestRequest(requestUrl, Method.GET);

            var response = await client.ExecuteTaskAsync<GooglePlusUser>(request, cancellationToken);
            if (!response.IsSuccessful)
            {
                throw new BadRequestException("Invalid oauth token");
            }
            var user = response.Data;

            user.PictureUrl = user.Image == null || user.Image.IsDefault
                ? null
                : new Uri(user.Image.Url).GetLeftPart(UriPartial.Path);

            return user;
        }
    }
}
