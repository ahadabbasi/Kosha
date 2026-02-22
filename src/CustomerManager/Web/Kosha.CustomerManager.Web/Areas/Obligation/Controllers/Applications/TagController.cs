using System;
using System.Collections.Generic;
using System.Linq;
using Kosha.CustomerManager.Web.Areas.Obligation.Models;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Tag;
using Kosha.CustomerManager.Web.Models.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Controllers.Applications;

[
    Route(RouteConfiguration.ApplicationRouteTemplate),
    ApiController,
    Area(AreaNameConfiguration.Obligation)
]
public sealed class TagController(ITagService service) : ControllerBase
{
    private readonly IEnumerable<ObligationTagVm> _tags = 
    [
        new(Id: 1, Name: "مهم"),
        new(Id: 2, Name: "فوری"),
        new(Id: 3, Name: "مالی"),
        new(Id: 4, Name: "حقوقی"),
        new(Id: 5, Name: "اداری"),
        new(Id: 6, Name: "فنی"),
        new(Id: 7, Name: "آموزشی"),
        new(Id: 8, Name: "پروژه‌ای"),
        new(Id: 9, Name: "دوره‌ای"),
        new(Id: 10, Name: "روزانه"),
        new(Id: 11, Name: "هفتگی"),
        new(Id: 12, Name: "ماهیانه"),
        new(Id: 13, Name: "فصلی"),
        new(Id: 14, Name: "سالانه"),
        new(Id: 15, Name: "قراردادی"),
        new(Id: 16, Name: "مشتری"),
        new(Id: 17, Name: "داخلی"),
        new(Id: 18, Name: "خارجی"),
        new(Id: 19, Name: "بایگانی"),
        new(Id: 20, Name: "در انتظار")
    ];

    [HttpGet("{id:guid}")]
    public IActionResult Index(Guid id) => Ok(new ObligationTagVm[] { new(21, "فروش") });


    [HttpGet]
    public IActionResult Search([FromQuery] ObligationTagVm tag) =>
        Ok(
            (
                !string.IsNullOrEmpty(tag.Name) ?
                    _tags.Where(item => !string.IsNullOrEmpty(item.Name) && item.Name.Contains(tag.Name, StringComparison.OrdinalIgnoreCase)) :
                    Enumerable.Empty<ObligationTagVm>()
            ).ToArray()
        );

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id, [FromBody] ObligationTagVm tag) => Ok();


    [HttpPost("{id:guid}")]
    public IActionResult Add(Guid id, [FromBody] ObligationTagVm tag) => Ok();
}