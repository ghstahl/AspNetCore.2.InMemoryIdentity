using Identity.Contrib.CoreTests;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace UnitTest.Identity.Contrib.InMemoryStore
{
    public static class HostContainer
    {
        private static ServiceProvider _serviceProvider;

        public static ServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
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
                    services.AddSingleton<UserRoleTestFactory>();
                    services.TryAddSingleton<IRoleTestFactory<ApplicationRole>>(x => x.GetService<UserRoleTestFactory>());
                    services.TryAddSingleton<IUserTestFactory<ApplicationUser>>(x => x.GetService<UserRoleTestFactory>());
                    services.TryAddSingleton<IUserRoleTestFactory<ApplicationUser>>(x => x.GetService<UserRoleTestFactory>());

                    _serviceProvider = services.BuildServiceProvider();
                }

                return _serviceProvider;
            }
        }
    }
}