using Kosha.CustomerManager.Web.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kosha.CustomerManager.Web.Infrastructure;

public static class InfrastructureStartup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        

        services.AddPersistence(configuration);

        return services;
    }
}
