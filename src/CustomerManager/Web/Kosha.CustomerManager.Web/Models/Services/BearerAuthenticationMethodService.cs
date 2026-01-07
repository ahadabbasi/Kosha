using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication.Models;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Models.Infrastructure.Helper;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
using Kosha.CustomerManager.Web.Models.ViewModels;
using Kosha.CustomerManager.Web.Shared.Helper.Time;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Kosha.CustomerManager.Web.Models.Services;

internal sealed class BearerAuthenticationMethodService(
    ITimeService timeService, 
    IOptions<BearerInformation> options,
    IUserService userService,
    AuthenticationClaimGeneratorService claimGeneratorService
) : IBearerAuthenticationMethodService
{
    private BearerInformation Information => options.Value;

    public async Task<Result<IAuthenticationSignInResponse>> SignInAsync(AuthenticationSignInRequest request, CancellationToken cancellation = default)
    {
        Result result = await userService.VerifyPasswordAsync(request, cancellation);

        BearerTokenVm? token = null;

        if (result)
        {
            IEnumerable<Claim> claims = 
                    await claimGeneratorService.CreateAsync(request.User, cancellation);

            DateTime expirationTime = timeService.Now.AddSeconds(Information.ValidationTerm);

            JwtSecurityToken securityToken =
                new JwtSecurityToken(
                    issuer: Information.Issuer,
                    audience: Information.Audience,
                    claims: claims,
                    expires: expirationTime,
                    signingCredentials:
                    new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Information.Key)
                        ),
                        SecurityAlgorithms.HmacSha256
                    )
                );

            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();

            token = 
                new BearerTokenVm(
                    securityTokenHandler.WriteToken(securityToken),
                    Information.ValidationTerm,
                    expirationTime
                );
        }

        return
            result || token == null
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