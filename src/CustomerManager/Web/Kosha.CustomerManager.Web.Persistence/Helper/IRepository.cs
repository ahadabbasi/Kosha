using System.Linq;

namespace Kosha.CustomerManager.Web.Persistence.Helper;

public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Get queryable entity
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> Query();

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
}