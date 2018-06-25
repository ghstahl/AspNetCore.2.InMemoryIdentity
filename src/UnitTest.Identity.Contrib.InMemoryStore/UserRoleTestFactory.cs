using System;
using System.Security.Cryptography;
using System.Text;
using Identity.Contrib.CoreTests;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Identity.Contrib.InMemoryStore
{
    public static class HashExtensions
    {
        public static string Sha256(this string value)
        {
            var message = Encoding.ASCII.GetBytes(value);
            SHA256Managed hashString = new SHA256Managed();
            string hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
    }
    public class UserRoleTestFactory : 
        IRoleTestFactory<ApplicationRole>,
        IUserTestFactory<ApplicationUser>,
        IUserRoleTestFactory<ApplicationUser>
    {
        private static UserRoleTestFactory _userRoleTestFactory;

        public static UserRoleTestFactory SingletonUserRoleTestFactory => _userRoleTestFactory ?? (_userRoleTestFactory = new UserRoleTestFactory());

       

        public HostContainer HostContainer { get; }
        public UserRoleTestFactory()
        {
            HostContainer = new HostContainer();
        }
        public ApplicationRole CreateRole()
        {
            var role = new ApplicationRole()
            {
                Claims = { new MemoryRoleClaim() { ClaimType = Guid.NewGuid().ToString(), ClaimValue = Guid.NewGuid().ToString() } },
                Name = Guid.NewGuid().ToString()
            };
            return role;
        }

        public IRoleStore<ApplicationRole> RoleStore => HostContainer.ServiceProvider.GetService<IRoleStore<ApplicationRole>>();
        public ApplicationUser CreateUser()
        {
            var userName = Guid.NewGuid().ToString();
           
            var user = new ApplicationUser()
            {
                Email = $"{Guid.NewGuid().ToString()}@a.com",
                UserName = userName,
                PhoneNumber = "8005551212",
                PasswordHash = "password".Sha256()
            };
            return user;
        }

        public IUserStore<ApplicationUser> UserStore => HostContainer.ServiceProvider.GetService<IUserStore<ApplicationUser>>();

        public IUserRoleStore<ApplicationUser> UserRoleStore => HostContainer.ServiceProvider.GetService<IUserRoleStore<ApplicationUser>>();
    }
}