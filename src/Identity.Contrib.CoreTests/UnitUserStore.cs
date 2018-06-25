using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Identity.Contrib.CoreTests
{
    public abstract class UnitUserStore<TUser> where TUser : IdentityUser
    {
        private IUserTestFactory<TUser> _userTestFactory;
        private IUserStore<TUser> _userStore;

        public UnitUserStore(IUserTestFactory<TUser> userTestFactory)
        {
            _userTestFactory = userTestFactory;
            _userStore = _userTestFactory.UserStore;
        }

        [TestMethod]
        public async Task UserStore_exists()
        {
            _userStore.ShouldNotBeNull();
        }

        [TestMethod]
        public async Task UserStore_normalize_apis()
        {
           
            var user = _userTestFactory.CreateUser();

            await _userStore.SetUserNameAsync(user,"dog", default(CancellationToken));
            var username = await _userStore.GetUserNameAsync(user, default(CancellationToken));

            await _userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));
            var normalized = await _userStore.GetNormalizedUserNameAsync(user, default(CancellationToken));
            normalized.ShouldMatch(username);
        }

        [TestMethod]
        public async Task UserStore_id_apis()
        {
          
            var user = _userTestFactory.CreateUser();

            var id = await _userStore.GetUserIdAsync(user, default(CancellationToken));
            id.ShouldMatch(user.Id);
        }

        [TestMethod]
        public async Task UserStore_create_find_by_name()
        {
         
            var user = _userTestFactory.CreateUser();
            var username = "dog";
            await _userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await _userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));

            await _userStore.CreateAsync(user, default(CancellationToken));
            var foundUser = await _userStore.FindByNameAsync(user.UserName, default(CancellationToken));

            foundUser.UserName.ShouldMatch(user.UserName);
        }

        [TestMethod]
        public async Task UserStore_create_find_by_id()
        {
           
            var user = _userTestFactory.CreateUser();
            var username = "dog";
            await _userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await _userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));

            await _userStore.CreateAsync(user, default(CancellationToken));
            var foundUser = await _userStore.FindByIdAsync(user.Id, default(CancellationToken));

            foundUser.UserName.ShouldMatch(user.UserName);
        }

        [TestMethod]
        public async Task UserStore_create_find_delete()
        {
           
            var user = _userTestFactory.CreateUser();
            var username = "dog";
            await _userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await _userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));

            await _userStore.CreateAsync(user, default(CancellationToken));
            var foundUser = await _userStore.FindByIdAsync(user.Id, default(CancellationToken));
            foundUser.UserName.ShouldMatch(username);

            await _userStore.DeleteAsync(user, default(CancellationToken));
            foundUser = await _userStore.FindByIdAsync(user.Id, default(CancellationToken));
            foundUser.ShouldBeNull();
        }

        [TestMethod]
        public async Task UserStore_create_find_update()
        {
            
            var user = _userTestFactory.CreateUser();
            var username = "dog";
            await _userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await _userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));

            await _userStore.CreateAsync(user, default(CancellationToken));
            var foundUser = await _userStore.FindByIdAsync(user.Id, default(CancellationToken));
            foundUser.UserName.ShouldMatch(username);

            username = "cat";
            await _userStore.SetUserNameAsync(user, username, default(CancellationToken));
            await _userStore.SetNormalizedUserNameAsync(user, username, default(CancellationToken));


            await _userStore.UpdateAsync(user, default(CancellationToken));
            foundUser = await _userStore.FindByIdAsync(user.Id, default(CancellationToken));
            foundUser.UserName.ShouldMatch(username);
        }
    }
}