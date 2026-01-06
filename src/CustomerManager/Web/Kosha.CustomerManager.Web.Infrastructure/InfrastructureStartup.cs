using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Tag;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Services;
using Kosha.CustomerManager.Web.Infrastructure.Services.Task;
using Kosha.CustomerManager.Web.Persistence;
using Kosha.CustomerManager.Web.Shared.Helper.Hasher.Algorithms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kosha.CustomerManager.Web.Infrastructure;

public static class InfrastructureStartup
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IHasherService, HasherService>();

        services.AddScoped<ITaskManagerService, TaskManagerService>();

        services.AddScoped<ITagService, TagService>();

        services.AddScoped<PaginateHelperService>();

        services.AddMediator(options =>
            {
                options.ServiceLifetime = ServiceLifetime.Scoped;
            }
        );

        services.AddPersistence(configuration);

        return services;
    }

    public static IHost UseInfrastructure(
        this IHost host, 
        IHostEnvironment env
    )
    {
        host.UsePersistence(env);

        return host;
    }
}
