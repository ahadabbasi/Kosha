using Kosha.CustomerManager.Web.Shared.Helper.Time;
using Kosha.CustomerManager.Web.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kosha.CustomerManager.Web.Shared;

public static class SharedStartup
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddScoped<ITimeService, TimeService>();

        return services;
    }
}