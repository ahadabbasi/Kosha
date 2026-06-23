using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Entities;

namespace Kosha.CustomerManager.Web.Persistence.Helper;

public interface ITagRepository : IEntityRepository<Tag>
{
    /// <summary>
    /// Checking the title already exist in the table or not
    /// </summary>
    /// <param name="title"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<bool> IsTitleExistAsync(string  title, CancellationToken cancellation = default);
}