using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;

public sealed record AuthenticationRefreshRequest(
    UserResponse User,
    string Password
) : AuthenticationSignInRequest(User, Password);