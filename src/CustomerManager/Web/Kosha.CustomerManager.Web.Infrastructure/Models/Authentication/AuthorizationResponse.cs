using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;

public sealed record AuthorizationResponse(
    Guid Id, 
    string? Username = null, 
    string? PhoneNumber = null, 
    string? Name =  null,
    string? Family = null
);