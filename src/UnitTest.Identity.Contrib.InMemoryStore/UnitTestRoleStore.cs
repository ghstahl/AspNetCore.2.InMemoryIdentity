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
    public class UnitTestRoleStore: UnitTestRoleStore<ApplicationRole>
    {
        public UnitTestRoleStore() : base(HostContainer.ServiceProvider.GetService<IRoleTestFactory<ApplicationRole>>())
        {
        }
    }
}
