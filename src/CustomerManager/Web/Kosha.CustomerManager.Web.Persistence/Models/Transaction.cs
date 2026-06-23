using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Microsoft.EntityFrameworkCore.Storage;

namespace Kosha.CustomerManager.Web.Persistence.Models;

internal sealed class Transaction : ITransaction
{
    internal Transaction(IDbContextTransaction transaction)
    {
        Disposed = false;
        _transaction = transaction;
    }

    private readonly IDbContextTransaction _transaction;

    private bool Disposed { get; set; }

    public void Dispose()
    {
        if (Disposed)
            return;

        Disposed = true;
        _transaction.Dispose();
    }

    public Task RollbackAsync(CancellationToken cancellation = default) =>
        !Disposed ? _transaction.RollbackAsync(cancellation) : Task.CompletedTask;

    public Task CommitAsync(CancellationToken cancellation = default) =>
        !Disposed ? _transaction.CommitAsync(cancellation) : Task.CompletedTask;
}