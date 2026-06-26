using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Obligation.Models;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Tag;
using Kosha.CustomerManager.Web.Infrastructure.Models.Tag;
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
public sealed class TagController(ITagService service) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Index(Guid id, CancellationToken cancellation)
    {
        Result<IEnumerable<TagResponse>> result = await service.FetchTaskTagsAsync(id, cancellation);
        
        return result && result.Data != null ? 
            Ok(result.Data.Select(Map)) : 
            BadRequest(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] ObligationCaptionVm tag, CancellationToken cancellation)
    {
        Result<IEnumerable<TagResponse>> result = await service.SearchTagsAsync(tag.Name, cancellation);

        return result && result.Data != null ?
            Ok(result.Data.Select(Map)) : 
            BadRequest(result.Errors);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromBody] ObligationCaptionVm tag, CancellationToken cancellation)
    {
        Result result = await service.DetachTagFromTaskAsync(id, tag.Id ?? Guid.Empty, cancellation);

        return result ?  Ok() : BadRequest(result.Errors);
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> Add(Guid id, [FromBody] ObligationCaptionVm tag, CancellationToken cancellation)
    {
        Result result = await service.AttachTagToTaskAsync(id, tag.Id ?? Guid.Empty, cancellation);

        return result ? Ok() : BadRequest(result.Errors);
    }

    private ObligationCaptionVm Map(TagResponse response) => 
        new(response.Id, response.Title);
}