using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;
using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Action;

public sealed record ActionResponse(
    Guid Id, 
    string Description,
    UserInformationResponse User, 
    DateTime LastTimeChanged
) : ActionRequest(Description);