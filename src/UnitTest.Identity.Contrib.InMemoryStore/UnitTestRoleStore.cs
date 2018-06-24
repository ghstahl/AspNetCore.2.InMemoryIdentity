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

        ApplicationRole CreateTestRole()
        {
            var role = new ApplicationRole()
            {
                Claims = {new MemoryRoleClaim() {ClaimType = "claim_one", ClaimValue = "claim_one_value"}},
                Name = "role_one"
            };
            return role;
        }

         [TestMethod]
        public async Task RoleStore_exists()
        {
            var roleStore = FetchRoleStore();
            roleStore.ShouldNotBeNull();
        }
        [TestMethod]
        public async Task RoleStore_create_find_by_id()
        {
            var roleStore = FetchRoleStore();
            var role = CreateTestRole();
            await roleStore.CreateAsync(role, default(CancellationToken));
            await roleStore.SetNormalizedRoleNameAsync(role,role.Name, default(CancellationToken));
            var fetchedRole = await roleStore.FindByIdAsync(role.Id, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);
        }
        [TestMethod]
        public async Task RoleStore_create_find_by_name()
        {
            var roleStore = FetchRoleStore();
            var role = CreateTestRole();
            await roleStore.CreateAsync(role, default(CancellationToken));
            await roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var fetchedRole = await roleStore.FindByNameAsync(role.Name, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);
        }
        [TestMethod]
        public async Task RoleStore_create_find_delete()
        {
            var roleStore = FetchRoleStore();
            var role = CreateTestRole();
            await roleStore.CreateAsync(role, default(CancellationToken));
            await roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var fetchedRole = await roleStore.FindByNameAsync(role.Name, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);

            await roleStore.DeleteAsync(role, default(CancellationToken));
            fetchedRole = await roleStore.FindByNameAsync(role.Name, default(CancellationToken));
            fetchedRole.ShouldBeNull();
        }
        [TestMethod]
        public async Task RoleStore_normalize_apis()
        {
            var roleStore = FetchRoleStore();
            var role = CreateTestRole();
            await roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var normalizedName = await roleStore.GetNormalizedRoleNameAsync(role, default(CancellationToken));
            role.Name.ShouldMatch(normalizedName);
        }
        [TestMethod]
        public async Task RoleStore_rolename_apis()
        {
            var roleStore = FetchRoleStore();
            var role = CreateTestRole();
            await roleStore.SetRoleNameAsync(role, "dog", default(CancellationToken));
            var normalizedName = await roleStore.GetRoleNameAsync(role, default(CancellationToken));
            normalizedName.ShouldMatch("dog");
        }
        [TestMethod]
        public async Task RoleStore_roleid_apis()
        {
            var roleStore = FetchRoleStore();
            var role = CreateTestRole();
            var id = await roleStore.GetRoleIdAsync(role, default(CancellationToken));
            id.ShouldMatch(role.Id);
        }
        [TestMethod]
        public async Task RoleStore_create_update()
        {
            var roleStore = FetchRoleStore();
            var role = CreateTestRole();
            await roleStore.CreateAsync(role, default(CancellationToken));
            await roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var fetchedRole = await roleStore.FindByNameAsync(role.Name, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);

            await roleStore.SetRoleNameAsync(role, "dog", default(CancellationToken));
            await roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            await roleStore.UpdateAsync(role, default(CancellationToken));
            fetchedRole = await roleStore.FindByNameAsync(role.Name, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);
        }
    }
}
