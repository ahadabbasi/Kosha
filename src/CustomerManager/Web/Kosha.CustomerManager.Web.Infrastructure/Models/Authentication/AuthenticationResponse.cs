using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;

public sealed record AuthenticationResponse(
    Guid Id,
    string Username, 
    string Password, 
    string? Name, 
    string? Family,
    string PhoneNumber
) : AuthenticationSaveRequest(Username, Password, Name, Family, PhoneNumber);