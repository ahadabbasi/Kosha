using System.Linq;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Controllers;

[
    Area(AreaNameConfiguration.Obligation),
    Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)
]
public sealed class HomeController(ITaskManagerService taskManagerService) : Controller
{
    public async Task<IActionResult> Index([Bind]PaginateRequest? request)
    {
        Result<PaginateResponse<ITaskPaginateResponse>> result =
            await taskManagerService.PaginateAsync(request);

        return 
            View(
                result && result.Data is not null ? 
                    result.Data : 
                    new PaginateResponse<ITaskPaginateResponse>(
                        Enumerable.Empty<ITaskPaginateResponse>(), 
                        0, 
                        0,
                        0,
                        0
                    )
            );
    }
}