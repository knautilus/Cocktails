using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cocktails.Security
{
    public class SecurityHelper
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string secretKey)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        }
    }
}
