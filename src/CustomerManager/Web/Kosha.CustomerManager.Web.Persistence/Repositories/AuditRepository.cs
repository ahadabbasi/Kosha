using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Helper;
using Kosha.CustomerManager.Web.Persistence.Contexts;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Persistence.Repositories;

internal class AuditRepository<TEntity>(ApplicationContext context) : Repository<TEntity>(context), IAuditRepository<TEntity>
    where TEntity : class, IAudit
{
    public virtual Task<TEntity?> GetByIdAsync(Guid id) => 
        Query().FirstOrDefaultAsync(PredicateId(id));

    public virtual async void Delete(Guid id)
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

    public virtual Task<bool> ExistsAsync(Guid id) 
        => Query().AnyAsync(PredicateId(id));

    protected virtual Expression<Func<TEntity, bool>> PredicateId(Guid id) 
        => e => e.Id == id;
}