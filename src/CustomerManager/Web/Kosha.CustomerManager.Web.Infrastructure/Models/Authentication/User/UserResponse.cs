using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;

public sealed record UserResponse(
    Guid Id,
    string Username, 
    string Password, 
    string? Name, 
    string? Family,
    string PhoneNumber
) : UserSaveRequest(Username, Password, Name, Family, PhoneNumber);