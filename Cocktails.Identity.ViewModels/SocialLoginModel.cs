namespace Cocktails.Identity.ViewModels
{
    public class SocialLoginModel
    {
        public LoginProviderType LoginProvider { get; set; }
        public string AccessToken { get; set; }
    }
}
