using System.Threading;
using System.Threading.Tasks;

namespace Kosha.CustomerManager.Web.Persistence.Helper;

public interface IUnitOfWork
{
    /// <summary>
    /// save all changes of database
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellation = default);
}