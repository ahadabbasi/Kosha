using Kosha.CustomerManager.Web.Persistence.Contexts;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Persistence.Interceptors;
using Kosha.CustomerManager.Web.Persistence.Repositories;
using Kosha.CustomerManager.Web.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kosha.CustomerManager.Web.Persistence;

public static class PersistenceStartup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IInterceptor, InsertedInterceptor>();
        services.AddScoped<IInterceptor, ModifiedInterceptor>();

        services.AddDbContext<ApplicationContext>((provider, options) =>
            {
                options.AddInterceptors(provider.GetServices<IInterceptor>());
                options.UseSqlServer(configuration.GetConnectionString("default"));
            }
        );

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddShared();

        return services;
    }
}