using System;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Controllers.Applications;

[
    Area(AreaNameConfiguration.Dashboard), Route(RouteConfiguration.ApplicationRouteTemplate),
    ApiController, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)
]
public sealed class TaskController(ITaskManagerService taskManagerService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CancellationToken cancellation)
    {
        Result<Guid> result = await taskManagerService.CreateAsync(cancellation);
        
        return result ? Ok(new TaskResponse(result)) : BadRequest(result.Errors);
    }
}