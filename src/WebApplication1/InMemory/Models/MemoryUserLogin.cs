using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.InMemory.Models
{
    public class MemoryUserLogin : IEquatable<MemoryUserLogin>, IEquatable<UserLoginInfo>
    {
        public MemoryUserLogin(UserLoginInfo loginInfo)
        {
            if (loginInfo == null)
            {
                throw new ArgumentNullException(nameof(loginInfo));
            }

            LoginProvider = loginInfo.LoginProvider;
            ProviderKey = loginInfo.ProviderKey;
            ProviderDisplayName = loginInfo.ProviderDisplayName;
        }

        public string LoginProvider { get; private set; }
        public string ProviderKey { get; private set; }
        public string ProviderDisplayName { get; private set; }

        public bool Equals(MemoryUserLogin other)
        {
            return other.LoginProvider.Equals(LoginProvider)
                   && other.ProviderKey.Equals(ProviderKey);
        }

        public bool Equals(UserLoginInfo other)
        {
            return other.LoginProvider.Equals(LoginProvider)
                   && other.ProviderKey.Equals(ProviderKey);
        }
    }
}
