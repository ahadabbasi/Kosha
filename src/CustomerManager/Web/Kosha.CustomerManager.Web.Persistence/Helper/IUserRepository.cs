using System.Collections.Generic;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Authenticate;

namespace Kosha.CustomerManager.Web.Persistence.Helper;

public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Find user by username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Task<User?> GetByUsernameAsync(string username);

    /// <summary>
    /// Check exist username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Task<bool> IsExistUsernameAsync(string username);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Task<IEnumerable<string>> UserRolesAsync(string username);
}