using Kosha.CustomerManager.Web.Persistence.Contexts;
using Kosha.CustomerManager.Web.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kosha.CustomerManager.Web.Persistence;

public static class PersistenceStartup
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("default"))
        );

        services.AddShared();

        return services;
    }
}