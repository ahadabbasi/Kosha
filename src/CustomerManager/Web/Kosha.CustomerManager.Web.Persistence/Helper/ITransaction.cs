using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kosha.CustomerManager.Web.Persistence.Helper;

public interface ITransaction : IDisposable
{
    Task RollbackAsync(CancellationToken cancellation = default);

    Task CommitAsync(CancellationToken cancellation = default);
}