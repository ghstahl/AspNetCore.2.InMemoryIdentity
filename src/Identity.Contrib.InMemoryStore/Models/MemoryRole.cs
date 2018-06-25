using System;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Identity
{
    /// <summary>
    ///     Represents a Role entity
    /// </summary>
    public class MemoryRole : MemoryRole<string>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public MemoryRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="roleName"></param>
        public MemoryRole(string roleName) : this()
        {
            Name = roleName;
        }
    }

    /// <summary>
    ///     Represents a Role entity
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class MemoryRole<TKey> : IdentityRole where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public MemoryRole()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="roleName"></param>
        public MemoryRole(string roleName) : this()
        {
            Name = roleName;
        }

        /// <summary>
        /// Navigation property for claims in the role
        /// </summary>
        public virtual ICollection<MemoryRoleClaim<TKey>> Claims { get; private set; } =
            new List<MemoryRoleClaim<TKey>>();

    }
}