namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;

public record AuthenticationSaveRequest(
    string Username,
    string Password,
    string Name,
    string Family,
    string PhoneNumber
) : AuthenticationUpdateRequest(Username, Name, Family, PhoneNumber);