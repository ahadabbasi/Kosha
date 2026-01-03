namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;

public record UserSaveRequest(
    string Username,
    string Password,
    string? Name,
    string? Family,
    string PhoneNumber
) : UserUpdateRequest(Username, Name, Family, PhoneNumber);