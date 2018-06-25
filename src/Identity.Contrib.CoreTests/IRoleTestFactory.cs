using Microsoft.AspNetCore.Identity;

namespace Identity.Contrib.CoreTests
{
    public interface IRoleTestFactory<TRole> where TRole : IdentityRole
    {
        TRole CreateRole();
        IRoleStore<TRole> RoleStore { get; }
    }
}