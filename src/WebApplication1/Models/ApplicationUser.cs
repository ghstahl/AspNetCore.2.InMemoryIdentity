using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication1.InMemory;
using AspNetCore2.Authentication.InMemoryStores;
using AspNetCore2.Authentication.InMemoryStores.Models;

namespace WebApplication1.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : MemoryIdentityUser
    {
        public ApplicationUser(string userName, string email) : base(userName, email)
        {
        }

        public ApplicationUser(string userName, MemoryUserEmail email) : base(userName, email)
        {
        }

        public ApplicationUser(string userName) : base(userName)
        {
        }
    }
}
