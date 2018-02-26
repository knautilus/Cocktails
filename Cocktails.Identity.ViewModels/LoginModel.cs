namespace Cocktails.Identity.ViewModels
{
    public class LoginModel : SocialLoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public bool IsSocial => LoginProvider != LoginProviderType.None; // TODO extension
    }
}
