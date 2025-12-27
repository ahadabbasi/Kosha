using Kosha.CustomerManager.Web.Models.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Controllers.Applications;

[
    Area(AreaNameConfiguration.Dashboard),
    Route(RouteConfiguration.ApplicationRouteTemplate),
    ApiController,
    Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)
]
public sealed class TaskController : ControllerBase
{
}