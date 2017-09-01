namespace Cocktails.Common.Models
{
    public class AuthSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int Lifetime { get; set; }
    }
}
