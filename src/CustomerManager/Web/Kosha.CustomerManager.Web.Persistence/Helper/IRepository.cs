using System;
using System.Linq;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Helper;

namespace Kosha.CustomerManager.Web.Persistence.Helper;

public interface IRepository<TEntity>
    where TEntity : class, IAudit
{
    /// <summary>
    /// Get queryable entity
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> Query();

    /// <summary>
    /// Find entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(Guid id);

    /// <summary>
    /// Add new entity
    /// </summary>
    /// <param name="entity"></param>
    void Add(TEntity entity);

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity"></param>
    void Update(TEntity entity);

    /// <summary>
    /// Remove entity
    /// </summary>
    /// <param name="entity"></param>
    void Delete(TEntity entity);

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