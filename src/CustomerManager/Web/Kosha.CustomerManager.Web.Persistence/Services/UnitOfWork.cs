using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Persistence.Contexts;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Persistence.Models;

namespace Kosha.CustomerManager.Web.Persistence.Services;

internal sealed class UnitOfWork(ApplicationContext context) : IUnitOfWork
{
    public ITransaction Transaction() => 
        new Transaction(context.Database.BeginTransaction());

    public Task<int> SaveChangesAsync(CancellationToken cancellation = default) => 
        context.SaveChangesAsync(cancellation);
}