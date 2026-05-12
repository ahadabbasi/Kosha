using System;
using System.Collections.Generic;
using System.Linq;
using Kosha.CustomerManager.Web.Areas.Obligation.Models;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Tag;
using Kosha.CustomerManager.Web.Models.Configurations;
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
public sealed class TagController(ITagService service) : ControllerBase
{
    private readonly IEnumerable<ObligationCaptionVm> _tags =
    [
        new(Id: Guid.CreateVersion7(), Name: "مهم"),
        new(Id: Guid.CreateVersion7(), Name: "فوری"),
        new(Id: Guid.CreateVersion7(), Name: "مالی"),
        new(Id: Guid.CreateVersion7(), Name: "حقوقی"),
        new(Id: Guid.CreateVersion7(), Name: "اداری"),
        new(Id: Guid.CreateVersion7(), Name: "فنی"),
        new(Id: Guid.CreateVersion7(), Name: "آموزشی"),
        new(Id: Guid.CreateVersion7(), Name: "پروژه‌ای"),
        new(Id: Guid.CreateVersion7(), Name: "دوره‌ای"),
        new(Id: Guid.CreateVersion7(), Name: "روزانه"),
        new(Id: Guid.CreateVersion7(), Name: "هفتگی"),
        new(Id: Guid.CreateVersion7(), Name: "ماهیانه"),
        new(Id: Guid.CreateVersion7(), Name: "فصلی"),
        new(Id: Guid.CreateVersion7(), Name: "سالانه"),
        new(Id: Guid.CreateVersion7(), Name: "قراردادی"),
        new(Id: Guid.CreateVersion7(), Name: "مشتری"),
        new(Id: Guid.CreateVersion7(), Name: "داخلی"),
        new(Id: Guid.CreateVersion7(), Name: "خارجی"),
        new(Id: Guid.CreateVersion7(), Name: "بایگانی"),
        new(Id: Guid.CreateVersion7(), Name: "در انتظار")
    ];

    [HttpGet("{id:guid}")]
    public IActionResult Index(Guid id) => Ok(new ObligationCaptionVm[] { new(Guid.CreateVersion7(), "فروش") });


    [HttpGet]
    public IActionResult Search([FromQuery] ObligationCaptionVm tag) =>
        Ok(
            (
                !string.IsNullOrEmpty(tag.Name) ?
                    _tags.Where(item => !string.IsNullOrEmpty(item.Name) && item.Name.Contains(tag.Name, StringComparison.OrdinalIgnoreCase)) :
                    Enumerable.Empty<ObligationCaptionVm>()
            ).ToArray()
        );

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id, [FromBody] ObligationCaptionVm tag)
        => Ok();


    [HttpPost("{id:guid}")]
    public IActionResult Add(Guid id, [FromBody] ObligationCaptionVm tag) => Ok();
}