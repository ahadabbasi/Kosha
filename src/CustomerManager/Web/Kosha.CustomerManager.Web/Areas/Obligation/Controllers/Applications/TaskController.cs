using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Obligation.Models;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Controllers.Applications;

[
    Route(RouteConfiguration.ApplicationRouteTemplate),
    ApiController, Area(AreaNameConfiguration.Obligation),
    Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)
]
public sealed class TaskController(ITaskManagerService taskManagerService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Index(Guid id, CancellationToken cancellation)
    {
        Result<ITaskDetailsResponse> result =
            await taskManagerService.DetailsAsync(id, cancellation);

        return 
            result && result.Data != null ? 
                Ok(
                    new ObligationDetailsVm(
                        result.Data.Title, 
                        (result.Data.Information ?? [])
                            .Select(item => 
                                new ObligationDetailsInformationVm(
                                    item.Key, 
                                    item.Value ?? string.Empty
                                )
                            )
                    )
                ) :
                BadRequest(result.Errors);
    }
}