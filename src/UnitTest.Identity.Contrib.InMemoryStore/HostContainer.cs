using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Identity.Contrib.InMemoryStore
{
    public class HostContainer
    {
        public HostContainer()
        {
            var services = new ServiceCollection();
            var inMemoryStore = new InMemoryStore<ApplicationUser, ApplicationRole>();

            services.AddSingleton<IUserStore<ApplicationUser>>(provider =>
            {
                return inMemoryStore;
            });
            services.AddSingleton<IUserRoleStore<ApplicationUser>>(provider =>
            {
                return inMemoryStore;
            });
            services.AddSingleton<IRoleStore<ApplicationRole>>(provider =>
            {
                return inMemoryStore;
            });

            services.AddLogging();


            ServiceProvider = services.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; }
    }
}