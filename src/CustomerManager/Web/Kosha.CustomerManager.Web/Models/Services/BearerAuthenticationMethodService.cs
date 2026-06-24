using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication.Models;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Models.Infrastructure.Helper;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
using Kosha.CustomerManager.Web.Models.ViewModels;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Models.Services;

internal sealed class BearerAuthenticationMethodService(
    AccessTokenService tokenService, 
    IUserService userService,
    AuthenticationClaimGeneratorService claimGeneratorService
) : IBearerAuthenticationMethodService
{

    public async Task<Result<IAuthenticationSignInResponse>> SignInAsync(AuthenticationSignInRequest request, CancellationToken cancellation = default)
    {
        Result result = await userService.VerifyPasswordAsync(request, cancellation);

        AccessTokenResponse? token = null;

        if (result)
            token =
                tokenService.Generate(
                    new AccessTokenRequest(
                        await claimGeneratorService.CreateAsync(request.User, cancellation)
                    )
                );

        return
            token == null
                ? Result.Failed<IAuthenticationSignInResponse>(
                    result.Errors.Any() ? result.Errors.ToArray() : [Error.None]
                )
                : Result.Success<IAuthenticationSignInResponse>(token);
    }

    public Task<Result<IAuthenticationSignOutResponse>> SignOutAsync(CancellationToken cancellation = default) => 
        Task.FromResult(Result.Success<IAuthenticationSignOutResponse>(new CookieAuthenticationVm()));

    public Task<Result<IAuthenticationRefreshResponse>> RefreshAsync(
        AuthenticationRefreshRequest request, 
        CancellationToken cancellation = default
    ) =>
        Task.FromResult(Result.Success<IAuthenticationRefreshResponse>(new CookieAuthenticationVm()));
    
}