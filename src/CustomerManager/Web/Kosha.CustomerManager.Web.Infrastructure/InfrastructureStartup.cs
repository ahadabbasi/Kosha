using Kosha.CustomerManager.Web.Infrastructure.Configurations.Options;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Action;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Category;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Customer;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Store;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Tag;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Services;
using Kosha.CustomerManager.Web.Infrastructure.Services.Customer;
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
        services.ConfigureOptions<FileInformationConfigureOption>();

        services.ConfigureOptions<ContactTaskInformationConfigureOption>();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IHasherService, HasherService>();

        services.AddScoped<ITaskManagerService, TaskManagerService>();

        services.AddScoped<ITagService, TagService>();

        services.AddScoped<IFileService, FileService>();

        services.AddScoped<ICustomerService, CustomerService>();

        services.AddScoped<ICustomerContactService, CustomerContactService>();

        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IActionService, ActionService>();

        services.AddScoped<PaginateHelperService>();

        services.AddMediator(options => 
            options.ServiceLifetime = ServiceLifetime.Scoped
        );

        return services.AddPersistence(configuration);
    }

    public static IHost UseInfrastructure(
        this IHost host, 
        IHostEnvironment env
    )
    {
        return host.UsePersistence(env);
    }
}
