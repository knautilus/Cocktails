namespace Cocktails.Identity.ViewModels
{
    public class ChangePasswordModel
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
