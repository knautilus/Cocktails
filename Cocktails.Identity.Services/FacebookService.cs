using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Identity.ViewModels;
using RestSharp;

namespace Cocktails.Identity.Services
{
    public class FacebookService
    {
        private const string FacebookDataUrl = "https://graph.facebook.com/me?fields=id,name,email,gender,birthday,picture&access_token={0}";

        public Task<FacebookUser> GetProfileAsync(string accessToken, CancellationToken cancellationToken)
        {
            //var imageSize = ImagesSettingsContainer
            //    .Settings
            //    .Entities
            //    .First(x => x.EntityType == EntityType.Users)
            //    .Sizes
            //    .First(x => x.Type == ImageSizeType.L);

            var requestUrl = string.Format(FacebookDataUrl, /*imageSize.Width, imageSize.Height,*/ accessToken);

            FacebookUser user;
            try
            {
                var client = new RestClient();
                var request = new RestRequest(new Uri(requestUrl), Method.GET);
                //request.RequestFormat = DataFormat.Json;
                //request.AddBody(new UserCreateModel
                //{
                //    access_token = token,
                //    users = new[] { new UserModel { email = userEmail, role_id = 1 } }
                //});

                user = client.Get<FacebookUser>(request).Data;

                //user = await _httpClient.GetAsync<FacebookUser>(requestUrl, cancellationToken);
            }
            catch (HttpRequestException)
            {
                throw new ArgumentException("Invalid oauth token");
            }

            user.PictureUrl = user.Picture.Data.Url;

            return Task.FromResult(user);
        }
    }
}
