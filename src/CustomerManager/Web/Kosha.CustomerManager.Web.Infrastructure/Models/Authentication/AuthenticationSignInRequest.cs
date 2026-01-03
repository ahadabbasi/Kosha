using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;

public record AuthenticationSignInRequest(
    UserResponse User,
    string Password,
    bool RememberMe
) : UserVerifiedPasswordRequest(User, Password);