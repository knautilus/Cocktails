using System.Collections.Generic;

namespace Cocktails.Data.Domain
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
        public virtual ICollection<UserRole> Roles { get; } = new List<UserRole>();

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<UserClaim> Claims { get; } = new List<UserClaim>();

        /// <summary>
        /// Navigation property for this users login accounts.
        /// </summary>
        public virtual ICollection<UserLogin> Logins { get; } = new List<UserLogin>();

        /// <summary>
        /// Navigation property for this users tokens.
        /// </summary>
        public virtual ICollection<UserToken> Tokens { get; } = new List<UserToken>();

        /// <summary>
        /// Returns the username for this user.
        /// </summary>
        public override string ToString()
        {
            return UserName;
        }
    }
}
