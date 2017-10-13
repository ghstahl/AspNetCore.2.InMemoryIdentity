using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.InMemory
{
    public class InMemoryUserStore<TUser> : 
        IUserStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserClaimStore<TUser>,
        IUserLoginStore<TUser> where TUser:IdentityUser
    {
        private Dictionary<string, TUser> userMap = new Dictionary<string, TUser>();
        private IList<TUser> userList = new List<TUser>();
        public Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            userMap[user.Id] = user;
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            userMap.Remove(user.Id);
            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {     
        }

        public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            TUser user;
            if (userMap.TryGetValue(userId, out user))
            {
                return Task.FromResult<TUser>(user);
            }
            return Task.FromResult<TUser>(null);
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task.FromResult<TUser>(null);
        }

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(user.Id);
        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(user.UserName);
        }

        public async Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
        {
           
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            userMap[user.Id] = user;
            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult("hi");
        }

        public Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
        {
            var appUser = userList.FirstOrDefault(x => x.UserName.Equals(user.UserName));
            if (appUser == null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return Task.FromResult<TUser>(null);
        }

        public async Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
