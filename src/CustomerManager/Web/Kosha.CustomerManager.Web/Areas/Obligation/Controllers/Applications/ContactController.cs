using System;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Obligation.Models;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Customer;
using Kosha.CustomerManager.Web.Infrastructure.Models.Customer;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Controllers.Applications;

[
    Route(RouteConfiguration.ApplicationRouteTemplate),
    ApiController,
    Area(AreaNameConfiguration.Obligation),
    Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)
]
public sealed class ContactController(ICustomerService service) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Index(Guid id, CancellationToken cancellation)
    {
        Result<CustomerResponse> result = await service.FetchTaskCustomerAsync(id, cancellation);
        
        return result && result.Data != null ? Ok(result.Data) : NotFound();
    }


    [HttpGet]
    public IActionResult Search([FromQuery] ObligationCaptionVm tag)
    {
        return Ok();
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> Add(Guid id, [FromBody] ObligationCaptionVm customer, CancellationToken cancellation)
    {
        Result result = await service.AssignCustomerToTaskAsync(id, customer.Id ?? Guid.Empty, cancellation);

        return result ? Ok() : BadRequest(result.Errors);
    }

    [HttpGet(RouteConfiguration.ActionName + RouteConfiguration.Separator + "{id:guid}")]
    public IActionResult Information(Guid id) =>
        Ok(
            new ObligationContactVm(
                "احد", 
                "عباسی", 
                [ 
                    new ObligationContactInformationVm("شماره همراه", "09120276307"),
                    new ObligationContactInformationVm("دفتر کار", "02186901268")
                ] 
            )
        );
}