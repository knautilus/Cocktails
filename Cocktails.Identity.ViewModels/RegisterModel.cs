namespace Cocktails.Identity.ViewModels
{
    public class RegisterModel : SocialLoginModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsSocial => LoginProvider != LoginProviderType.None; // TODO extension
    }
}
