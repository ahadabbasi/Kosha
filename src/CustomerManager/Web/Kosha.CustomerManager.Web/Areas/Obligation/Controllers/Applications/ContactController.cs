using System;
using System.Collections.Generic;
using System.Linq;
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
    ApiController, Area(AreaNameConfiguration.Obligation),
    Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)
]
public sealed class ContactController(ICustomerService service) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Index(Guid id, CancellationToken cancellation)
    {
        Result<CustomerResponse> result = await service.FetchTaskCustomerAsync(id, cancellation);
        
        return result && result.Data != null ?
            Ok(Map(result.Data)) : 
            NotFound();
    }


    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] ObligationCaptionVm tag, CancellationToken cancellation = default)
    {
        Result<IEnumerable<CustomerResponse>> result = 
            await service.SearchCustomerAsync(tag.Name, cancellation);
        
        return result && result.Data != null ? Ok(result.Data.Select(Map)) : BadRequest(result.Errors);
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> Add(Guid id, [FromBody] ObligationCaptionVm customer, CancellationToken cancellation)
    {
        Result result = await service.AssignCustomerToTaskAsync(id, customer.Id ?? Guid.Empty, cancellation);

        return result ? Ok() : BadRequest(result.Errors);
    }

    [HttpGet(RouteConfiguration.ActionName + RouteConfiguration.Separator + "{id:guid}")]
    public async Task<IActionResult> Information(Guid id, CancellationToken cancellation)
    {
        Result<CustomerInformationResponse> result =
            await service.TaskCustomerInformationAsync(id, cancellation);

        return
            result && result.Data != null
                ? Ok(
                    new ObligationContactVm(
                        result.Data.Name,
                        result.Data.Family,
                        (result.Data.Contacts ?? [])
                            .Select(contact => new ObligationDetailsInformationVm(contact.Type, contact.Value))
                    )
                )
                : BadRequest(result.Errors);
    }

    private ObligationCaptionVm Map(CustomerResponse response) =>
        new(
            response.Id,
            string.Concat(response.Name, " ", response.Family)
        );
}