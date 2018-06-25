using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Identity.Contrib.CoreTests
{
    [TestClass]
 
    public abstract class UnitTestUserRoleStore<TUser,TRole> 
        where TUser : IdentityUser
        where TRole : IdentityRole
    {
        private IRoleTestFactory<TRole> _roleTestFactory;
        private IRoleStore<TRole> _roleStore;
        private IUserTestFactory<TUser> _userTestFactory;
        private IUserStore<TUser> _userStore;
        private IUserRoleTestFactory<TUser> _userRoleTestFactory;
        private IUserRoleStore<TUser> _userRoleStore;
        public UnitTestUserRoleStore(
            IUserTestFactory<TUser> userTestFactory, 
            IRoleTestFactory<TRole> roleTestFactory,
            IUserRoleTestFactory<TUser> userRoleTestFactory)
        {
            _userTestFactory = userTestFactory;
            _userStore = _userTestFactory.UserStore;
            _roleTestFactory = roleTestFactory;
            _roleStore = roleTestFactory.RoleStore;
            _userRoleTestFactory = userRoleTestFactory;
            _userRoleStore = userRoleTestFactory.UserRoleStore;
        }

        [TestMethod]
        public async Task UserRoleStore_exists()
        {
            _userStore.ShouldNotBeNull();
            _roleStore.ShouldNotBeNull();
            _userRoleStore.ShouldNotBeNull();
        }

        [TestMethod]
        public async Task UserRoleStore_create_user_add_to_role()
        {
            
            var user = _userTestFactory.CreateUser();
            var username = "dog";
            await _userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await _userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));

            await _userStore.CreateAsync(user, default(CancellationToken));
            var foundUser = await _userStore.FindByIdAsync(user.Id, default(CancellationToken));

            foundUser.UserName.ShouldMatch(user.UserName);

            var role = _roleTestFactory.CreateRole();
            await _roleStore.CreateAsync(role, default(CancellationToken));
            await _roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));

            await _userRoleStore.AddToRoleAsync(user,role.Name, default(CancellationToken));

            var roles = await _userRoleStore.GetRolesAsync(user, default(CancellationToken));
            roles.Count.ShouldBeGreaterThan(0);

            var users = await _userRoleStore.GetUsersInRoleAsync(role.Name, default(CancellationToken));
            users.Count.ShouldBeGreaterThan(0);

            var firstUser = users.FirstOrDefault();
            firstUser.ShouldNotBeNull();
            firstUser.UserName.ShouldMatch(user.UserName);
        }
        [TestMethod]
        public async Task UserRoleStore_create_user_add_to_role_remove()
        {
 
            var user = _userTestFactory.CreateUser();
            var username = "dog";
            await _userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await _userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));

            await _userStore.CreateAsync(user, default(CancellationToken));
            var foundUser = await _userStore.FindByIdAsync(user.Id, default(CancellationToken));
            foundUser.UserName.ShouldMatch(user.UserName);

            var role = _roleTestFactory.CreateRole();
            await _roleStore.CreateAsync(role, default(CancellationToken));
            await _roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));

            await _userRoleStore.AddToRoleAsync(user, role.Name, default(CancellationToken));

            var roles = await _userRoleStore.GetRolesAsync(user, default(CancellationToken));
            roles.Count.ShouldBeGreaterThan(0);

            var users = await _userRoleStore.GetUsersInRoleAsync(role.Name, default(CancellationToken));
            users.Count.ShouldBeGreaterThan(0);

            var firstUser = users.FirstOrDefault();
            firstUser.ShouldNotBeNull();
            firstUser.UserName.ShouldMatch(user.UserName);

            await _userRoleStore.RemoveFromRoleAsync(user, role.Name, default(CancellationToken));

            roles = await _userRoleStore.GetRolesAsync(user, default(CancellationToken));
            roles.Count.ShouldBe(0);

            users = await _userRoleStore.GetUsersInRoleAsync(role.Name, default(CancellationToken));
            users.Count.ShouldBe(0);

        }
    }
}