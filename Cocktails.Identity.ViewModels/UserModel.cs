using Cocktails.Common.Models;

namespace Cocktails.Identity.ViewModels
{
    public class UserModel : BaseModel<long>
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
