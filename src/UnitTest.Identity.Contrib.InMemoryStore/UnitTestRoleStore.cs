using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace UnitTest.Identity.Contrib.InMemoryStore
{
    [TestClass]
    public class UnitTestRoleStore
    {
        public HostContainer HostContainer { get; }

        public ServiceProvider ServiceProvider
        {
            get { return HostContainer.ServiceProvider; }
        }

        public UnitTestRoleStore()
        {
            HostContainer = new HostContainer();
        }

        IRoleStore<ApplicationRole> FetchRoleStore()
        {
            return ServiceProvider.GetService<IRoleStore<ApplicationRole>>();
        }

        [TestMethod]
        public async Task RoleStore_exists()
        {
            var store = FetchRoleStore();
            store.ShouldNotBeNull();
        }

        [TestMethod]
        public async Task RoleStore_create_find_by_id()
        {
            var store = FetchRoleStore();
            var role = Utils.CreateTestRole();
            await store.CreateAsync(role, default(CancellationToken));
            await store.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var fetchedRole = await store.FindByIdAsync(role.Id, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);
        }

        [TestMethod]
        public async Task RoleStore_create_find_by_name()
        {
            var store = FetchRoleStore();
            var role = Utils.CreateTestRole();
            await store.CreateAsync(role, default(CancellationToken));
            await store.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var fetchedRole = await store.FindByNameAsync(role.Name, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);
        }

        [TestMethod]
        public async Task RoleStore_create_find_delete()
        {
            var store = FetchRoleStore();
            var role = Utils.CreateTestRole();
            await store.CreateAsync(role, default(CancellationToken));
            await store.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var fetchedRole = await store.FindByNameAsync(role.Name, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);

            await store.DeleteAsync(role, default(CancellationToken));
            fetchedRole = await store.FindByNameAsync(role.Name, default(CancellationToken));
            fetchedRole.ShouldBeNull();
        }

        [TestMethod]
        public async Task RoleStore_normalize_apis()
        {
            var store = FetchRoleStore();
            var role = Utils.CreateTestRole();
            await store.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var normalizedName = await store.GetNormalizedRoleNameAsync(role, default(CancellationToken));
            role.Name.ShouldMatch(normalizedName);
        }

        [TestMethod]
        public async Task RoleStore_rolename_apis()
        {
            var store = FetchRoleStore();
            var role = Utils.CreateTestRole();
            await store.SetRoleNameAsync(role, "dog", default(CancellationToken));
            var normalizedName = await store.GetRoleNameAsync(role, default(CancellationToken));
            normalizedName.ShouldMatch("dog");
        }

        [TestMethod]
        public async Task RoleStore_roleid_apis()
        {
            var store = FetchRoleStore();
            var role = Utils.CreateTestRole();
            var id = await store.GetRoleIdAsync(role, default(CancellationToken));
            id.ShouldMatch(role.Id);
        }

        [TestMethod]
        public async Task RoleStore_create_update()
        {
            var store = FetchRoleStore();
            var role = Utils.CreateTestRole();
            await store.CreateAsync(role, default(CancellationToken));
            await store.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var fetchedRole = await store.FindByNameAsync(role.Name, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);

            await store.SetRoleNameAsync(role, "dog", default(CancellationToken));
            await store.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            await store.UpdateAsync(role, default(CancellationToken));
            fetchedRole = await store.FindByNameAsync(role.Name, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);
        }
    }
}
