using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace UnitTest.Identity.Contrib.InMemoryStore
{
    public static class Utils
    {
        public static ApplicationUser CreateTestUser()
        {
            var user = new ApplicationUser()
            {
                Email = "a@a.com",
                UserName = "a",
                PhoneNumber = "8005551212",
                PasswordHash = "1234"
            };
            return user;
        }
        public static ApplicationRole CreateTestRole()
        {
            var role = new ApplicationRole()
            {
                Claims = { new MemoryRoleClaim() { ClaimType = "claim_one", ClaimValue = "claim_one_value" } },
                Name = "role_one"
            };
            return role;
        }
    }
}
