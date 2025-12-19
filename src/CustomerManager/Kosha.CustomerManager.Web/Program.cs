using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kosha.CustomerManager.Web;

public static class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        Startup.ConfigurationServices(builder.Services, builder.Configuration);

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        Startup.Configuration(app, app.Environment);

        await app.RunAsync();
    }
}