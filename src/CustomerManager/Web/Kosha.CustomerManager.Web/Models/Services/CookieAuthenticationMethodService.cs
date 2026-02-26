using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication.Models;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Models.Infrastructure.Helper;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
using Kosha.CustomerManager.Web.Models.ViewModels;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Kosha.CustomerManager.Web.Models.Services;

internal sealed class CookieAuthenticationMethodService(
    IUserService userService,
    IHttpContextAccessor accessor,
    AuthenticationClaimGeneratorService claimGeneratorService,
    AccessTokenService accessTokenService
) : ICookieAuthenticationMethodService
{
    private HttpContext Context => accessor.HttpContext ?? throw new InvalidOperationException();

    internal const string RememberMeClaimType = ClaimTypes.IsPersistent;

    internal const string AccessTokenClaimType = ClaimTypes.Authentication;

    public async Task<Result<IAuthenticationSignInResponse>> SignInAsync(AuthenticationSignInRequest request, CancellationToken cancellation = default)
    {
        Result result = await userService.VerifyPasswordAsync(request, cancellation);

        if (result) 
            await MakeSignInAsync(request);

        return
            !result
                ? Result.Failed<IAuthenticationSignInResponse>(
                    result.Errors.Any() ? result.Errors.ToArray() : [Error.None]
                )
                : Result.Success<IAuthenticationSignInResponse>(new CookieAuthenticationVm());
    }

    public async Task<Result<IAuthenticationSignOutResponse>> SignOutAsync(CancellationToken cancellation = default)
    {
        await Context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Result.Success<IAuthenticationSignOutResponse>(new CookieAuthenticationVm());
    }

    public async Task<Result<IAuthenticationRefreshResponse>> RefreshAsync(AuthenticationRefreshRequest request, CancellationToken cancellation = default)
    {
        string? rememberMeValue = Context.User.FindFirstValue(RememberMeClaimType);

        bool rememberMe = false;

        if (
            !string.IsNullOrEmpty(rememberMeValue) && 
            bool.TryParse(rememberMeValue, out rememberMe)
        )
        {
            //
        }

        await SignOutAsync(cancellation);

        await MakeSignInAsync(
            new AuthenticationSignInRequest(
                request.User,
                string.Empty,
                rememberMe
            )
        );

        return Result.Success<IAuthenticationRefreshResponse>(new CookieAuthenticationVm());
    }

    public Result<string> AccessToken()
    {
        Result<string> result =
            Result.Failed<string>(Error.None);

        string? token = Context.User.FindFirstValue(AccessTokenClaimType);

        if(!string.IsNullOrEmpty(token))
            result = Result.Success(token);

        return result;
    }

    private async Task MakeSignInAsync(AuthenticationSignInRequest request)
    {
        IEnumerable<Claim> claims =
            await claimGeneratorService.CreateAsync(request.User);

        string[] validAccessTokenClaim = 
            [
                ClaimDefinitionConfiguration.Identifier, 
                ClaimDefinitionConfiguration.Role
            ];

        AccessTokenResponse token = 
            accessTokenService.Generate(
                new AccessTokenRequest(
                    claims.Where(item => validAccessTokenClaim.Contains(item.Type)).ToArray()
                )
            );

        claims =
            claims.Concat(
                [
                    new Claim(
                        RememberMeClaimType,
                        request.RememberMe.ToString()
                    ),
                    new Claim(
                        AccessTokenClaimType,
                        token.Token
                    )
                ]
            );

        await Context.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(
                [
                    new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme
                    )
                ]
            ),
            new AuthenticationProperties { IsPersistent = request.RememberMe }
        );
    }
}