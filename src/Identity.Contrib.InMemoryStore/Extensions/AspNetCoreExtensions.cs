using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Identity.Extensions
{
    public static class AspNetCoreExtensions
    {
        public static IdentityBuilder AddInMemoryIdentity<TUser, TRole>(this IServiceCollection services)
            where TUser : MemoryUser
            where TRole : MemoryRole
        {
            var inMemoryStore = new InMemoryStore<TUser, TRole>();

            services.AddSingleton<IUserStore<TUser>>(provider => inMemoryStore);
            services.AddSingleton<IUserRoleStore<TUser>>(provider => inMemoryStore);
            services.AddSingleton<IRoleStore<TRole>>(provider => inMemoryStore);
            return services.AddIdentity<TUser, TRole>();
        }
    }
}
