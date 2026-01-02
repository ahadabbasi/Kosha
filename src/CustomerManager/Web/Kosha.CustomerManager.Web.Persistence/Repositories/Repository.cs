using System.Linq;
using Kosha.CustomerManager.Web.Persistence.Contexts;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Persistence.Repositories;

internal class Repository<TEntity>(ApplicationContext context) : IRepository<TEntity>
    where TEntity : class
{
    protected DbSet<TEntity> Set { get; } = context.Set<TEntity>();

    protected ApplicationContext Context { get; } = context;

    public virtual IQueryable<TEntity> Query() => Set;

    public virtual void Add(TEntity entity)
    {
        Set.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        Set.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        Set.Remove(entity);
    }
}