using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Obligation.Models;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Action;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Action;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Shared.Helper.Time;
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
public sealed class ActionController(
    IActionService actionService, IPersianService persianService,
    IAuthorizeService authorizeService
) : ControllerBase
{
    /*
    private readonly ObligationActionVm[] _actions =
    [
        new(
            "وظیفه جدید ایجاد شد و به تیم مالی ارجاع داده شد.",
            "علی محمدی",
            persianService.Parse(DateTime.Parse("2025-02-15 09:30:00"))
        ),
        new(
            "بررسی اولیه انجام شد و برای تایید نهایی ارسال گردید.",
            "زهرا حسینی",
            persianService.Parse(DateTime.Parse("2025-02-16 11:45:00"))
        ),
        new(
            "مدارک پیوست شده ناقص است. درخواست اصلاح ارسال شد.",
            "محمد کریمی",
            persianService.Parse(DateTime.Parse("2025-02-17 14:20:00"))
        ),
        new(
            "وظیفه با موفقیت تکمیل شد و مستندات آپلود گردید.",
            "فاطمه رضایی",
            persianService.Parse(DateTime.Parse("2025-02-18 10:15:00"))
        ),
        new(
            "تغییرات درخواستی اعمال و برای بازبینی مجدد ارسال شد.",
            "رضا نوروزی",
            persianService.Parse(DateTime.Parse("2025-02-19 16:30:00"))
        )
    ];
    */

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Index(Guid id, CancellationToken cancellation)
    {
        Result<IEnumerable<ActionResponse>> resultAction = 
            await actionService.FetchTaskActionsAsync(id, cancellation);

        IEnumerable<ActionResponse> data = [];

        if (resultAction && resultAction.Data != null)
            data = resultAction.Data;

        Guid? user = null;

        if (data.Any())
        {
            Result<AuthorizationResponse> resultUser =
                authorizeService.Authenticate();

            if (resultUser && resultUser.Data != null)
                user = resultUser.Data.Id;
        }

        return
            Ok(
                data.Select(item => 
                    new ObligationActionVm(
                        item.Description, 
                        string.Concat(item.User.Name, " ", item.User.Family),
                        persianService.Parse(item.LastTimeChanged),
                        user is not null && item.User.Id.Equals(user)
                    )
                )
            );
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> Add(Guid id, [FromBody]ObligationActionVm entry, CancellationToken cancellation)
    {
        Result result =
            await actionService.AppendActionToTaskAsync(
                id, new ActionRequest(entry.Comment), cancellation
            );

        return result ? Ok() : BadRequest(result.Errors);
    }
}