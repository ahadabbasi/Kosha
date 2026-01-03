using Kosha.CustomerManager.Web.Shared.Models.Hasher.Default;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;

public record UserVerifiedPasswordRequest(
    UserResponse User,
    string Password
) : HasherRequest(Password);