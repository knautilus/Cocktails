using System.Collections.Generic;

namespace Cocktails.Data.Identity
{
    public class User : BaseEntity<long>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }

        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public ICollection<UserRole> Roles { get; } = new List<UserRole>();

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public ICollection<UserClaim> Claims { get; } = new List<UserClaim>();

        /// <summary>
        /// Navigation property for this users login accounts.
        /// </summary>
        public ICollection<UserLogin> Logins { get; } = new List<UserLogin>();

        /// <summary>
        /// Navigation property for this users tokens.
        /// </summary>
        public ICollection<UserToken> Tokens { get; } = new List<UserToken>();

        public UserProfile UserProfile { get; set; } = new UserProfile();

        /// <summary>
        /// Returns the username for this user.
        /// </summary>
        public override string ToString()
        {
            return UserName;
        }
    }
}
