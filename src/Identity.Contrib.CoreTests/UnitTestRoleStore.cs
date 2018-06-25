using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Identity.Contrib.CoreTests
{
    public abstract class UnitTestRoleStore<TRole> where TRole : IdentityRole
    {
        private IRoleTestFactory<TRole> _roleTestFactory;
        private IRoleStore<TRole> _roleStore;

        public UnitTestRoleStore(IRoleTestFactory<TRole> roleTestFactory )
        {
            _roleTestFactory = roleTestFactory;
            _roleStore = roleTestFactory.RoleStore;
        }
     

        [TestMethod]
        public async Task RoleStore_exists()
        {
            _roleStore.ShouldNotBeNull();
        }
        [TestMethod]
        public async Task RoleStore_create_find_by_id()
        {
            var role = _roleTestFactory.CreateRole();
            await _roleStore.CreateAsync(role, default(CancellationToken));
            await _roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var fetchedRole = await _roleStore.FindByIdAsync(role.Id, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);
        }

        [TestMethod]
        public async Task RoleStore_create_find_by_name()
        {
         
            var role = _roleTestFactory.CreateRole();
            await _roleStore.CreateAsync(role, default(CancellationToken));
            await _roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var fetchedRole = await _roleStore.FindByNameAsync(role.Name, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);
        }

        [TestMethod]
        public async Task RoleStore_create_find_delete()
        {
          
            var role = _roleTestFactory.CreateRole();
            await _roleStore.CreateAsync(role, default(CancellationToken));
            await _roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var fetchedRole = await _roleStore.FindByNameAsync(role.Name, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);

            await _roleStore.DeleteAsync(role, default(CancellationToken));
            fetchedRole = await _roleStore.FindByNameAsync(role.Name, default(CancellationToken));
            fetchedRole.ShouldBeNull();
        }

        [TestMethod]
        public async Task RoleStore_normalize_apis()
        {
            var role = _roleTestFactory.CreateRole();
            await _roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var normalizedName = await _roleStore.GetNormalizedRoleNameAsync(role, default(CancellationToken));
            role.Name.ShouldMatch(normalizedName);
        }

        [TestMethod]
        public async Task RoleStore_rolename_apis()
        {
            var role = _roleTestFactory.CreateRole();
            await _roleStore.SetRoleNameAsync(role, "dog", default(CancellationToken));
            var normalizedName = await _roleStore.GetRoleNameAsync(role, default(CancellationToken));
            normalizedName.ShouldMatch("dog");
        }

        [TestMethod]
        public async Task RoleStore_roleid_apis()
        {
            var role = _roleTestFactory.CreateRole();
            var id = await _roleStore.GetRoleIdAsync(role, default(CancellationToken));
            id.ShouldMatch(role.Id);
        }

        [TestMethod]
        public async Task RoleStore_create_update()
        {
            var role = _roleTestFactory.CreateRole();
            await _roleStore.CreateAsync(role, default(CancellationToken));
            await _roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            var fetchedRole = await _roleStore.FindByNameAsync(role.Name, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);

            await _roleStore.SetRoleNameAsync(role, "dog", default(CancellationToken));
            await _roleStore.SetNormalizedRoleNameAsync(role, role.Name, default(CancellationToken));
            await _roleStore.UpdateAsync(role, default(CancellationToken));
            fetchedRole = await _roleStore.FindByNameAsync(role.Name, default(CancellationToken));
            role.Name.ShouldMatch(fetchedRole.Name);
        }
    }
}
