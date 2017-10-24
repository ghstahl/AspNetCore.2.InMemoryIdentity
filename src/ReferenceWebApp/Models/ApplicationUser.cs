using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore2.Authentication.InMemoryStores;
using AspNetCore2.Authentication.InMemoryStores.Models;
using Microsoft.AspNetCore.Identity;

namespace ReferenceWebApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : MemoryUser
    {
    }
}
