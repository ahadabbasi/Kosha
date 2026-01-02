using Kosha.CustomerManager.Web.Shared.Helper.Hasher.Algorithms;
using Kosha.CustomerManager.Web.Shared.Models.Hasher.Default;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;

public record AuthenticationVerifiedPasswordRequest(
    AuthenticationResponse User,
    string Password
) : HasherRequest(Password);