using System.Threading;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication.Models;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Shared.Results;
using System.Threading.Tasks;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;

public interface IAuthenticationMethodService
{
    /// <summary>
    /// Create sign in base their method
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<IAuthenticationSignInResponse>> SignInAsync(
        AuthenticationSignInRequest  request, 
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Sign out the user has been sign in
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<IAuthenticationSignOutResponse>> SignOutAsync(
        CancellationToken cancellation = default
    );


    /// <summary>
    /// Refresh sign in user
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<IAuthenticationRefreshResponse>> RefreshAsync(
        AuthenticationSignInRequest request,
        CancellationToken cancellation = default
    );
}