namespace Cocktails.Identity.ViewModels
{
    public class LoginModel
    {
        public LoginProviderType LoginProvider { get; set; }
        public string AccessToken { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
