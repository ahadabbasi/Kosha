namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;

public record UserUpdateRequest(
    string Username,
    string? Name,
    string? Family,
    string PhoneNumber
) : UserRequest(Username);