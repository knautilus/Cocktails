namespace Cocktails.Identity.ViewModels
{
    public class ResetPasswordModel
    {
        public string UserId { get; set; }

        public string NewPassword { get; set; }

        public string Code { get; set; }
    }
}
