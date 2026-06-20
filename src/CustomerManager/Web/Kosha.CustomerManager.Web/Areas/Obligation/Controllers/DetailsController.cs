using System;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Obligation.Models;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Controllers;

[
    Area(AreaNameConfiguration.Obligation),
    Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme),
    Route(RouteConfiguration.AreaName + RouteConfiguration.Separator + RouteConfiguration.ControllerName)
]
public sealed class DetailsController(ITaskManagerService taskManagerService) : Controller
{
    [HttpGet($"{{{RouteConfiguration.IdName}:guid}}")]
    public async Task<IActionResult> Index(Guid id, [Bind] TaskPaginationRequest? request)
    {
        Result<PaginateResponse<ITaskPaginateResponse>> result =
            await taskManagerService.PaginateAsync(request);

        return 
            View(
                new ObligationTaskVm(
                    id, 
                    result && result.Data != null ?
                        result.Data : 
                        new PaginateResponse<ITaskPaginateResponse>(
                            [], 0,
                            0,0,0
                        )
                )
            );
    }
}