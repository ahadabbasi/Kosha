using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication.Models;
using System;

namespace Kosha.CustomerManager.Web.Models.Infrastructure.Models;

public sealed record AccessTokenResponse(
    string Token,
    int ValidationTerm,
    DateTime ExpirationTime
) : IAuthenticationSignInResponse,
    IAuthenticationRefreshResponse;