using System;
using System.Collections.Generic;
using System.Linq;
using Kosha.CustomerManager.Web.Areas.Obligation.Models;
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
public sealed class ContactController : ControllerBase
{
    private readonly IEnumerable<ObligationCaptionVm> _contacts =
    [
        new(Id: Guid.CreateVersion7(), Name: "علی محمدی"),
        new(Id: Guid.CreateVersion7(), Name: "زهرا حسینی"),
        new(Id: Guid.CreateVersion7(), Name: "محمد کریمی"),
        new(Id: Guid.CreateVersion7(), Name: "فاطمه رضایی"),
        new(Id: Guid.CreateVersion7(), Name: "حسین احمدی"),
        new(Id: Guid.CreateVersion7(), Name: "مریم السادات موسوی"),
        new(Id: Guid.CreateVersion7(), Name: "رضا نوروزی"),
        new(Id: Guid.CreateVersion7(), Name: "سارا محمدپور"),
        new(Id: Guid.CreateVersion7(), Name: "مهدی رحمانی"),
        new(Id: Guid.CreateVersion7(), Name: "نرگس صالحی"),
        new(Id: Guid.CreateVersion7(), Name: "حمیدرضا کاظمی"),
        new(Id: Guid.CreateVersion7(), Name: "لیلا حیدری"),
        new(Id: Guid.CreateVersion7(), Name: "سعید طاهری"),
        new(Id: Guid.CreateVersion7(), Name: "الهام شکوری"),
        new(Id: Guid.CreateVersion7(), Name: "مجتبی عزیزی"),
        new(Id: Guid.CreateVersion7(), Name: "پریسا فرهادی"),
        new(Id: Guid.CreateVersion7(), Name: "امیر عباسی"),
        new(Id: Guid.CreateVersion7(), Name: "سمانه قدیری"),
        new(Id: Guid.CreateVersion7(), Name: "جواد میرزایی"),
        new(Id: Guid.CreateVersion7(), Name: "مینا کوهی")
    ];

    [HttpGet("{id:guid}")]
    public IActionResult Index(Guid id) => NotFound();


    [HttpGet]
    public IActionResult Search([FromQuery] ObligationCaptionVm tag) =>
        Ok(
            (
                !string.IsNullOrEmpty(tag.Name) ?
                    _contacts.Where(item => !string.IsNullOrEmpty(item.Name) && item.Name.Contains(tag.Name, StringComparison.OrdinalIgnoreCase)) :
                    []
            ).ToArray()
        );

    [HttpPost("{id:guid}")]
    public IActionResult Add(Guid id, [FromBody] ObligationCaptionVm tag) 
        => Ok();

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