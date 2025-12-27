using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Configurations;

public class ErrorConfiguration
{
    public static readonly Error UsernameOrPasswordIsWrong =
        new(
            nameof(UsernameOrPasswordIsWrong),
            "Username or password is wrong."
        );
}