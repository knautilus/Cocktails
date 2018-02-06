using System.Collections.Generic;

namespace Cocktails.Data.Domain
{
    /// <summary>
    /// Represents a role in the identity system
    /// </summary>
    public class Role : BaseEntity<long>
    {
        public Role() { }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        public Role(string roleName) : this()
        {
            Name = roleName;
        }

        /// <summary>
        /// Navigation property for the users in this role.
        /// </summary>
        public ICollection<UserRole> Users { get; } = new List<UserRole>();

        /// <summary>
        /// Navigation property for claims in this role.
        /// </summary>
        public ICollection<RoleClaim> Claims { get; } = new List<RoleClaim>();

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns the name of the role.
        /// </summary>
        /// <returns>The name of the role.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
