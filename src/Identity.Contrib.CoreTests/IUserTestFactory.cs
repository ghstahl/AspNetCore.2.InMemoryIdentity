using Microsoft.AspNetCore.Identity;

namespace Identity.Contrib.CoreTests
{
    public interface IUserTestFactory<TUser> where TUser : IdentityUser
    {
        TUser CreateUser();
        IUserStore<TUser> UserStore { get; }
    }
}