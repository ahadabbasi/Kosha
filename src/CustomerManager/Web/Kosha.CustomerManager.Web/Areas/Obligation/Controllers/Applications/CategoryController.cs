using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Obligation.Models;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Category;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Models.Category;
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
public sealed class CategoryController(
    ITaskManagerService taskService, ICategoryService categoryService
) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Index(Guid id, CancellationToken cancellation)
    {
        Result<Guid> resultCategory = await taskService.CategoryAsync(id, cancellation);

        Result<CategoryResponse> result =
            Result.Failed<CategoryResponse>(
                resultCategory.Errors.Any() ? resultCategory.Errors.ToArray() : [Error.None]
            );

        if (resultCategory) 
            result = await categoryService.FindByIdAsync(resultCategory, cancellation);

        return result && result.Data != null ? Ok(Map(result.Data)) : BadRequest(result.Errors);
    }

    public async Task<IActionResult> List(CancellationToken cancellation) => 
        Ok((await categoryService.ListAsync(cancellation)).Select(Map));

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> Change(Guid id, [FromBody] ObligationCaptionVm tag, CancellationToken cancellation)
    {
        Result result = await taskService.ChangeCategoryAsync(id, tag.Id ?? Guid.Empty, cancellation);

        return result ? Ok() : BadRequest(result.Errors);
    }

    private ObligationCaptionVm Map(CategoryResponse response) =>
        new(response.Id, response.Name);
}