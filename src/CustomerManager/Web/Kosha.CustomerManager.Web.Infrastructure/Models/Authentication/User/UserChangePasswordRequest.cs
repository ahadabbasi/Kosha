namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;

public record UserChangePasswordRequest(
    UserResponse User,
    string OldPassword,
    string NewPassword
) : UserVerifiedPasswordRequest(User, OldPassword);