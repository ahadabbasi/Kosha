using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Authenticate;

namespace Kosha.CustomerManager.Web.Persistence.Helper;

public interface IUserRepository : IAuditRepository<User>
{
    /// <summary>
    /// Find user by username
    /// </summary>
    /// <param name="username"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellation = default);

    /// <summary>
    /// Check exist username
    /// </summary>
    /// <param name="username"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<bool> IsExistUsernameAsync(string username, CancellationToken cancellation = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<IEnumerable<string>> UserRolesAsync(string username, CancellationToken cancellation = default);
}