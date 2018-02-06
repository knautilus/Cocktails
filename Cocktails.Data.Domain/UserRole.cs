using System;

namespace Cocktails.Data.Domain
{
    /// <summary>
    /// Represents the link between a user and a role.
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// Gets or sets the primary key of the user that is linked to a role.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the primary key of the role that is linked to the user.
        /// </summary>
        public long RoleId { get; set; }
    }
}
