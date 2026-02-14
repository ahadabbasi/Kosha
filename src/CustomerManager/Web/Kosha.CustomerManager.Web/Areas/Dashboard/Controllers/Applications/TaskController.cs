using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Models.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Controllers.Applications;

[
    Area(AreaNameConfiguration.Dashboard),
    Route(RouteConfiguration.ApplicationRouteTemplate),
    ApiController,
    //Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)
]
public sealed class TaskController(ITaskManagerService taskManagerService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CancellationToken cancellation)
        => Ok(await taskManagerService.CreateAsync(cancellation));
}