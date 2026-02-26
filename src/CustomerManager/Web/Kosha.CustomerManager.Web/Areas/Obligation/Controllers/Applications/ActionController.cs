using System;
using Kosha.CustomerManager.Web.Areas.Obligation.Models;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Shared.Helper.Time;
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
public sealed class ActionController(IPersianService persianService) : ControllerBase
{
    private readonly ObligationActionVm[] _actions =
    [
        new(
            "وظیفه جدید ایجاد شد و به تیم مالی ارجاع داده شد.",
            "علی محمدی",
            persianService.ConvertToPersianDateTime(DateTime.Parse("2025-02-15 09:30:00"))
        ),
        new(
            "بررسی اولیه انجام شد و برای تایید نهایی ارسال گردید.",
            "زهرا حسینی",
            persianService.ConvertToPersianDateTime(DateTime.Parse("2025-02-16 11:45:00"))
        ),
        new(
            "مدارک پیوست شده ناقص است. درخواست اصلاح ارسال شد.",
            "محمد کریمی",
            persianService.ConvertToPersianDateTime(DateTime.Parse("2025-02-17 14:20:00"))
        ),
        new(
            "وظیفه با موفقیت تکمیل شد و مستندات آپلود گردید.",
            "فاطمه رضایی",
            persianService.ConvertToPersianDateTime(DateTime.Parse("2025-02-18 10:15:00"))
        ),
        new(
            "تغییرات درخواستی اعمال و برای بازبینی مجدد ارسال شد.",
            "رضا نوروزی",
            persianService.ConvertToPersianDateTime(DateTime.Parse("2025-02-19 16:30:00"))
        )
    ];

    [HttpGet("{id:guid}")]
    public IActionResult Index(Guid id) => Ok(_actions);

    [HttpPost("{id:guid}")]
    public IActionResult Add(Guid id) => Ok();
}