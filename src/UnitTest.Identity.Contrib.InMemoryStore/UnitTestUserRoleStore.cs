using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Identity.Contrib.CoreTests;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace UnitTest.Identity.Contrib.InMemoryStore
{
    [TestClass]
    public class UnitTestUserRoleStore: UnitTestUserRoleStore<ApplicationUser, ApplicationRole>
    {
        public UnitTestUserRoleStore() : 
            base(UserRoleTestFactory.SingletonUserRoleTestFactory, UserRoleTestFactory.SingletonUserRoleTestFactory, UserRoleTestFactory.SingletonUserRoleTestFactory)
        {
        }
    }
}

 