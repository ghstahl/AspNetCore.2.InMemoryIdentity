using System;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Identity
{
    /// <summary>
    /// Memory user class
    /// </summary>
    public class MemoryUser : MemoryUser<string>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MemoryUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userName"></param>
        public MemoryUser(string userName) : this()
        {
            UserName = userName;
        }
    }

    /// <summary>
    /// Memory user
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class MemoryUser<TKey> : IdentityUser where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public MemoryUser()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userName"></param>
        public MemoryUser(string userName) : this()
        {
            UserName = userName;
        }

        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual ICollection<MemoryUserRole<TKey>> Roles { get; private set; } = new List<MemoryUserRole<TKey>>();

        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual ICollection<MemoryUserClaim<TKey>> Claims { get; private set; } =
            new List<MemoryUserClaim<TKey>>();

        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual ICollection<MemoryUserLogin<TKey>> Logins { get; private set; } =
            new List<MemoryUserLogin<TKey>>();

        /// <summary>
        /// Navigation property
        /// </summary>
        public virtual ICollection<MemoryUserToken<TKey>> Tokens { get; private set; } =
            new List<MemoryUserToken<TKey>>();
    }
}