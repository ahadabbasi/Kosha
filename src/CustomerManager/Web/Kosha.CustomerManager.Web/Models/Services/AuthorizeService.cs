using System;
using System.Security.Claims;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Http;

namespace Kosha.CustomerManager.Web.Models.Services;

internal sealed class AuthorizeService(IHttpContextAccessor accessor) : IAuthorizeService
{
    private HttpContext Context => accessor.HttpContext ?? throw new InvalidOperationException();

    public Result IsAuthenticate() => 
        Context.User.Identity is not null && Context.User.Identity.IsAuthenticated;

    public Result<AuthorizationResponse> Authenticate()
    {
        Result<AuthorizationResponse> result = Result.Failed<AuthorizationResponse>(Error.None);

        if (IsAuthenticate())
        {
            string? identifier = Context.User.FindFirstValue(ClaimDefinitionConfiguration.Identifier);

            if (
                !string.IsNullOrEmpty(identifier) && 
                Guid.TryParse(identifier, out Guid value)
            )
                result =
                    Result.Success(
                        new AuthorizationResponse(
                            value,
                            Context.User.FindFirstValue(ClaimDefinitionConfiguration.Username),
                            Context.User.FindFirstValue(ClaimDefinitionConfiguration.PhoneNumber),
                            Context.User.FindFirstValue(ClaimDefinitionConfiguration.Name),
                            Context.User.FindFirstValue(ClaimDefinitionConfiguration.Family)
                        )
                    );
        }

        return result;
    }
}