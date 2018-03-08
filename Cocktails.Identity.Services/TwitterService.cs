using Cocktails.Identity.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cocktails.Identity.Services
{
    public class TwitterService : ISocialService<TwitterUser>
    {
        private readonly TwitterKeys _keys;
        private TweetSharp.TwitterService _service;
        private TweetSharp.TwitterService Service
        {
            get
            {
                if (_service == null)
                    _service = new TweetSharp.TwitterService(_keys.Key, _keys.Secret);
                return _service;
            }
        }

        public TwitterService(TwitterKeys keys)
        {
            _keys = keys;
        }

        public Task<TwitterUser> GetProfileAsync(string accessToken, CancellationToken cancellationToken)
        {
            var tokenData = accessToken.Split(';');

            var requestToken = new TweetSharp.OAuthRequestToken { Token = tokenData[0] }; // TODO Are TokenSecret, OAuthCallbackConfirmed needed?

            var authAccessToken = Service.GetAccessToken(requestToken, tokenData[1]);
            var user = GetSocialAuthByNativeToken(authAccessToken.Token, authAccessToken.TokenSecret);
            return Task.FromResult(user);
        }

        public TwitterUser GetSocialAuthByNativeToken(string accessToken, string accessTokenSecret)
        {
            Service.AuthenticateWith(accessToken, accessTokenSecret);
            var user = Service.VerifyCredentials(new TweetSharp.VerifyCredentialsOptions());
            if (user == null)
                throw new Exception("twitter return null user");

            var model = new TwitterUser();

            model.Id = user.Id.ToString();
            model.Name = user.Name;
            model.Email = user.Email;
            model.PictureUrl = user.ProfileImageUrl;

            return model;
        }
    }
}
