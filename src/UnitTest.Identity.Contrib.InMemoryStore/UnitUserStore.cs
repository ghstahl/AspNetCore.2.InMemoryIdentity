using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace UnitTest.Identity.Contrib.InMemoryStore
{
    [TestClass]
    public class UnitUserStore
    {
        public HostContainer HostContainer { get; }

        public ServiceProvider ServiceProvider
        {
            get { return HostContainer.ServiceProvider; }
        }

        public UnitUserStore()
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

        [TestMethod]
        public async Task UserStore_exists()
        {
            var userStore = FetchUserStore();
            var userRoleStore = FetchUserRoleStore();
            userStore.ShouldNotBeNull();
            userRoleStore.ShouldNotBeNull();
        }

        [TestMethod]
        public async Task UserStore_normalize_apis()
        {
            var userStore = FetchUserStore();
            var user = Utils.CreateTestUser();

            await userStore.SetUserNameAsync(user,"dog", default(CancellationToken));
            var username = await userStore.GetUserNameAsync(user, default(CancellationToken));

            await userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));
            var normalized = await userStore.GetNormalizedUserNameAsync(user, default(CancellationToken));
            normalized.ShouldMatch(username);
        }

        [TestMethod]
        public async Task UserStore_id_apis()
        {
            var userStore = FetchUserStore();
            var user = Utils.CreateTestUser();

            var id = await userStore.GetUserIdAsync(user, default(CancellationToken));
            id.ShouldMatch(user.Id);
        }

        [TestMethod]
        public async Task UserStore_create_find_by_name()
        {
            var userStore = FetchUserStore();
            var user = Utils.CreateTestUser();
            var username = "dog";
            await userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));

            await userStore.CreateAsync(user, default(CancellationToken));
            var foundUser = await userStore.FindByNameAsync(user.UserName, default(CancellationToken));

            foundUser.UserName.ShouldMatch(user.UserName);
        }

        [TestMethod]
        public async Task UserStore_create_find_by_id()
        {
            var userStore = FetchUserStore();
            var user = Utils.CreateTestUser();
            var username = "dog";
            await userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));

            await userStore.CreateAsync(user, default(CancellationToken));
            var foundUser = await userStore.FindByIdAsync(user.Id, default(CancellationToken));

            foundUser.UserName.ShouldMatch(user.UserName);
        }

        [TestMethod]
        public async Task UserStore_create_find_delete()
        {
            var userStore = FetchUserStore();
            var user = Utils.CreateTestUser();
            var username = "dog";
            await userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));

            await userStore.CreateAsync(user, default(CancellationToken));
            var foundUser = await userStore.FindByIdAsync(user.Id, default(CancellationToken));
            foundUser.UserName.ShouldMatch(username);

            await userStore.DeleteAsync(user, default(CancellationToken));
            foundUser = await userStore.FindByIdAsync(user.Id, default(CancellationToken));
            foundUser.ShouldBeNull();
        }

        [TestMethod]
        public async Task UserStore_create_find_update()
        {
            var userStore = FetchUserStore();
            var user = Utils.CreateTestUser();
            var username = "dog";
            await userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));

            await userStore.CreateAsync(user, default(CancellationToken));
            var foundUser = await userStore.FindByIdAsync(user.Id, default(CancellationToken));
            foundUser.UserName.ShouldMatch(username);

            username = "cat";
            await userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));


            await userStore.UpdateAsync(user, default(CancellationToken));
            foundUser = await userStore.FindByIdAsync(user.Id, default(CancellationToken));
            foundUser.UserName.ShouldMatch(username);
        }
    }
}