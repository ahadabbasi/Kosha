using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;

public interface IAuthenticationService
{
    /// <summary>
    /// Find user by username
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<AuthenticationResponse>> FindByUsernameAsync(AuthenticationRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// get roles of user
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<IEnumerable<string>>> RolesAsync(AuthenticationRequest request, CancellationToken cancellationToken = default);
}