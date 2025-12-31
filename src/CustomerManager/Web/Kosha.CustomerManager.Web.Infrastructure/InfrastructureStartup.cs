using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Hasher.Algorithms;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Services;
using Kosha.CustomerManager.Web.Infrastructure.Services.Task;
using Kosha.CustomerManager.Web.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kosha.CustomerManager.Web.Infrastructure;

public static class InfrastructureStartup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IHasherService, HasherService>();

        services.AddScoped<ITaskManagerService, TaskManagerService>();

        services.AddMediator(options =>
            {
                options.ServiceLifetime = ServiceLifetime.Scoped;
            }
        );

        services.AddPersistence(configuration);

        return services;
    }
}
