using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Entities;
using Kosha.CustomerManager.Web.Persistence.Contexts;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Persistence.Repositories;

internal sealed class TagRepository(
    ApplicationContext context
) : AuditRepository<Tag>(context), 
    ITagRepository
{
    public Task<bool> IsTitleExistAsync(string title, CancellationToken cancellation = default) => 
        Query()
            .AnyAsync(
                item => item.Title.Equals(title),
                cancellation
            );
}