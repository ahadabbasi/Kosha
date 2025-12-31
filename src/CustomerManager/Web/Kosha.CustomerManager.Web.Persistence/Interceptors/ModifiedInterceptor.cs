using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Helper;
using Kosha.CustomerManager.Web.Shared.Helper.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Kosha.CustomerManager.Web.Persistence.Interceptors;

internal sealed class ModifiedInterceptor(ITimeService timeService) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result
    )
    {
        ModifyEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken()
    )
    {
        ModifyEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ModifyEntities(DbContext? context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified)
            .Where(entityEntry => entityEntry.Entity is IModified);

        foreach (EntityEntry entry in entries)
        {
            if(entry.Entity is IModified modifiedEntity)
                modifiedEntity.Modified = timeService.Now;
        }
    }
}