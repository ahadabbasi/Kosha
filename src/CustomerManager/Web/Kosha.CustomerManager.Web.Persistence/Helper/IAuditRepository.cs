using System;
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
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(Guid id);

    /// <summary>
    /// Remove entity by id
    /// </summary>
    /// <param name="id"></param>
    void Delete(Guid id);

    /// <summary>
    /// Check entity existence by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(Guid id);
}