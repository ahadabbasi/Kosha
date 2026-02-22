using System;
using System.Collections.Generic;
using System.Linq;
using Kosha.CustomerManager.Web.Areas.Obligation.Models;
using Kosha.CustomerManager.Web.Models.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Controllers.Applications;

[
    Route(RouteConfiguration.ApplicationRouteTemplate),
    ApiController,
    Area(AreaNameConfiguration.Obligation)
]
public sealed class ContactController : ControllerBase
{
    private readonly IEnumerable<ObligationTagVm> _contacts =
    [
        new(Id: 1, Name: "علی محمدی"),
        new(Id: 2, Name: "زهرا حسینی"),
        new(Id: 3, Name: "محمد کریمی"),
        new(Id: 4, Name: "فاطمه رضایی"),
        new(Id: 5, Name: "حسین احمدی"),
        new(Id: 6, Name: "مریم السادات موسوی"),
        new(Id: 7, Name: "رضا نوروزی"),
        new(Id: 8, Name: "سارا محمدپور"),
        new(Id: 9, Name: "مهدی رحمانی"),
        new(Id: 10, Name: "نرگس صالحی"),
        new(Id: 11, Name: "حمیدرضا کاظمی"),
        new(Id: 12, Name: "لیلا حیدری"),
        new(Id: 13, Name: "سعید طاهری"),
        new(Id: 14, Name: "الهام شکوری"),
        new(Id: 15, Name: "مجتبی عزیزی"),
        new(Id: 16, Name: "پریسا فرهادی"),
        new(Id: 17, Name: "امیر عباسی"),
        new(Id: 18, Name: "سمانه قدیری"),
        new(Id: 19, Name: "جواد میرزایی"),
        new(Id: 20, Name: "مینا کوهی")
    ];

    [HttpGet("{id:guid}")]
    public IActionResult Index(Guid id) => NotFound();


    [HttpGet]
    public IActionResult Search([FromQuery] ObligationTagVm tag) =>
        Ok(
            (
                !string.IsNullOrEmpty(tag.Name) ?
                    _contacts.Where(item => !string.IsNullOrEmpty(item.Name) && item.Name.Contains(tag.Name, StringComparison.OrdinalIgnoreCase)) :
                    Enumerable.Empty<ObligationTagVm>()
            ).ToArray()
        );

    [HttpPost("{id:guid}")]
    public IActionResult Add(Guid id, [FromBody] ObligationTagVm tag) => Ok();
}