using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication.Models;

namespace Kosha.CustomerManager.Web.Models.ViewModels;

public sealed record CookieAuthenticationVm : 
    IAuthenticationSignInResponse, 
    IAuthenticationSignOutResponse, 
    IAuthenticationRefreshResponse;