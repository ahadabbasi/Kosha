using System;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Helper;

namespace Kosha.CustomerManager.Web.Persistence.Helper;

public interface IAuditRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IAudit
{
    /// <summary>
    /// Find entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellation = default);

    /// <summary>
    /// Remove entity by id
    /// </summary>
    /// <param name="id"></param>
    void Delete(Guid id);

    /// <summary>
    /// Check entity existence by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellation = default);
}