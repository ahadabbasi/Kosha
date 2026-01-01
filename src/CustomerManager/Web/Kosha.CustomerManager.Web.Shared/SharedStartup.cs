using Kosha.CustomerManager.Web.Shared.Helper.Time;
using Kosha.CustomerManager.Web.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kosha.CustomerManager.Web.Shared;

public static class SharedStartup
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddScoped<ITimeService, TimeService>();

        return services;
    }

    public static IHost SharedConfiguration(
        this IHost host,
        IHostEnvironment env
    )
    {
        return host;
    }
}