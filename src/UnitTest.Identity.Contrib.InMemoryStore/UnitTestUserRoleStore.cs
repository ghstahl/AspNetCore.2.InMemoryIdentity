using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace UnitTest.Identity.Contrib.InMemoryStore
{
    [TestClass]
    public class UnitTestUserRoleStore
    {
        public HostContainer HostContainer { get; }

        public ServiceProvider ServiceProvider
        {
            get { return HostContainer.ServiceProvider; }
        }

        public UnitTestUserRoleStore()
        {
            HostContainer = new HostContainer();
        }

        IUserRoleStore<ApplicationUser> FetchUserRoleStore()
        {
            return ServiceProvider.GetService<IUserRoleStore<ApplicationUser>>();
        }
        IUserStore<ApplicationUser> FetchUserStore()
        {
            return ServiceProvider.GetService<IUserStore<ApplicationUser>>();
        }
        IRoleStore<ApplicationRole> FetchRoleStore()
        {
            return ServiceProvider.GetService<IRoleStore<ApplicationRole>>();
        }
        [TestMethod]
        public async Task UserRoleStore_exists()
        {
            var userStore = FetchUserStore();
            var userRoleStore = FetchUserRoleStore();
            userStore.ShouldNotBeNull();
            userRoleStore.ShouldNotBeNull();
        }

        [TestMethod]
        public async Task UserRoleStore_create_user_add_to_role()
        {
            var roleStore = FetchRoleStore();
            var userStore = FetchUserStore();
            var userRoleStore = FetchUserRoleStore();
            var user = Utils.CreateTestUser();
            var username = "dog";
            await userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));

            await userStore.CreateAsync(user, default(CancellationToken));
            var foundUser = await userStore.FindByIdAsync(user.Id, default(CancellationToken));

            foundUser.UserName.ShouldMatch(user.UserName);

            var role = Utils.CreateTestRole();
            await roleStore.CreateAsync(role, default(CancellationToken));
            await roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));

            await userRoleStore.AddToRoleAsync(user,role.Name, default(CancellationToken));

            var roles = await userRoleStore.GetRolesAsync(user, default(CancellationToken));
            roles.Count.ShouldBeGreaterThan(0);

            var users = await userRoleStore.GetUsersInRoleAsync(role.Name, default(CancellationToken));
            users.Count.ShouldBeGreaterThan(0);

            var firstUser = users.FirstOrDefault();
            firstUser.ShouldNotBeNull();
            firstUser.UserName.ShouldMatch(user.UserName);
        }
        [TestMethod]
        public async Task UserRoleStore_create_user_add_to_role_remove()
        {
            var roleStore = FetchRoleStore();
            var userStore = FetchUserStore();
            var userRoleStore = FetchUserRoleStore();
            var user = Utils.CreateTestUser();
            var username = "dog";
            await userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));

            await userStore.CreateAsync(user, default(CancellationToken));
            var foundUser = await userStore.FindByIdAsync(user.Id, default(CancellationToken));
            foundUser.UserName.ShouldMatch(user.UserName);

            var role = Utils.CreateTestRole();
            await roleStore.CreateAsync(role, default(CancellationToken));
            await roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));

            await userRoleStore.AddToRoleAsync(user, role.Name, default(CancellationToken));

            var roles = await userRoleStore.GetRolesAsync(user, default(CancellationToken));
            roles.Count.ShouldBeGreaterThan(0);

            var users = await userRoleStore.GetUsersInRoleAsync(role.Name, default(CancellationToken));
            users.Count.ShouldBeGreaterThan(0);

            var firstUser = users.FirstOrDefault();
            firstUser.ShouldNotBeNull();
            firstUser.UserName.ShouldMatch(user.UserName);

            await userRoleStore.RemoveFromRoleAsync(user, role.Name, default(CancellationToken));

            roles = await userRoleStore.GetRolesAsync(user, default(CancellationToken));
            roles.Count.ShouldBe(0);

            users = await userRoleStore.GetUsersInRoleAsync(role.Name, default(CancellationToken));
            users.Count.ShouldBe(0);

        }
    }
}