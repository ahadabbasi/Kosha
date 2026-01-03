using Kosha.CustomerManager.Web.Persistence.Contexts;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Persistence.Interceptors;
using Kosha.CustomerManager.Web.Persistence.Repositories;
using Kosha.CustomerManager.Web.Persistence.Services;
using Kosha.CustomerManager.Web.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

        services.AddScoped(typeof(IAuditRepository<>), typeof(AuditRepository<>));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddShared();

        return services;
    }

    public static IHost UsePersistence(
        this IHost host,
        IHostEnvironment env
    )
    {
        /*
        if (env.IsDevelopment())
        {
            host.UseMigration<ApplicationContext>();
        }

        host.UseSeeder(typeof(PersistenceStartup).Assembly);
        */

        host.UseShared(env);

        return host;
    }
}