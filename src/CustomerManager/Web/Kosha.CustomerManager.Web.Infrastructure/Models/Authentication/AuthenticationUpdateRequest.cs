namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;

public record AuthenticationUpdateRequest(
    string Username,
    string? Name,
    string? Family,
    string PhoneNumber
) : AuthenticationRequest(Username);