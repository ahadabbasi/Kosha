using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;

public sealed record UserInformationResponse(
    Guid Id, string Username, 
    string? Name, string? Family
);