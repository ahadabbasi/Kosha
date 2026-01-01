using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Persistence.Contexts;
using Kosha.CustomerManager.Web.Persistence.Helper;

namespace Kosha.CustomerManager.Web.Persistence.Services;

internal sealed class UnitOfWork(ApplicationContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellation = default) 
        => context.SaveChangesAsync(cancellation);
}