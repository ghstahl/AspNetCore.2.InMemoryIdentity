using Microsoft.AspNetCore.Identity;

namespace Identity.Contrib.CoreTests
{
    public interface IUserRoleTestFactory<TUser> where TUser : IdentityUser
    {
        IUserRoleStore<TUser> UserRoleStore { get; }
    }
}