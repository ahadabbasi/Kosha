using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kosha.CustomerManager.Web.Persistence.Extensions;

public static class UseMigrationExtension
{
    public static void UseMigration<TContext>(this IHost entry)
        where TContext : DbContext
    {
        try
        {
            using (IServiceScope scope = entry.Services.CreateScope())
            {
                TContext? context =
                    scope.ServiceProvider.GetService<TContext>();

                if (context != null && context.Database.GetPendingMigrations().Any())
                {
                    /*
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    */
                    context.Database.Migrate();
                }
            }
        }
        catch
        {
            //
        }
    }
}