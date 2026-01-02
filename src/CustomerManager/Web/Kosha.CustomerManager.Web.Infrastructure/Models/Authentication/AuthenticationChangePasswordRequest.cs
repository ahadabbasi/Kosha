namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;

public record AuthenticationChangePasswordRequest(
    AuthenticationResponse User,
    string OldPassword,
    string NewPassword
) : AuthenticationVerifiedPasswordRequest(User, OldPassword);