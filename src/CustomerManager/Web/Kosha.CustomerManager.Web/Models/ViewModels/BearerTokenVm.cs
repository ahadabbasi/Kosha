using System;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication.Models;

namespace Kosha.CustomerManager.Web.Models.ViewModels;

public sealed record BearerTokenVm(
    string Token, 
    int ValidationTerm, 
    DateTime ExpirationTime
) : IAuthenticationSignInResponse, 
    IAuthenticationRefreshResponse;