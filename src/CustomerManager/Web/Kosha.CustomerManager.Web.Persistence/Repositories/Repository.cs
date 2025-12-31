using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Helper;
using Kosha.CustomerManager.Web.Persistence.Contexts;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Persistence.Repositories;

internal class Repository<TEntity>(ApplicationContext context) : IRepository<TEntity>
    where TEntity : class, IAudit
{
    protected DbSet<TEntity> Set { get; } = context.Set<TEntity>();

    protected ApplicationContext Context { get; } = context;

    public IQueryable<TEntity> Query() => Set;

    public Task<TEntity?> GetByIdAsync(Guid id) => 
        Query().FirstOrDefaultAsync(PredicateId(id));

    public void Add(TEntity entity)
    {
        Set.Add(entity);
    }

    public void Update(TEntity entity)
    {
        Set.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        Set.Remove(entity);
    }

    public async void Delete(Guid id)
    {
        try
        {
            TEntity? entity = await GetByIdAsync(id);

            if (entity is not null)
            {
                Delete(entity);
            }
        }
        catch
        {
            //
        }
    }

    public Task<bool> ExistsAsync(Guid id) 
        => Query().AnyAsync(PredicateId(id));


    protected Expression<Func<TEntity, bool>> PredicateId(Guid id) 
        => e => e.Id == id;
}