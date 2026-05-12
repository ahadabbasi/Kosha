using System;
using Kosha.CustomerManager.Web.Models.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Controllers;

[
    Area(AreaNameConfiguration.Obligation),
    Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme),
    Route(RouteConfiguration.AreaName + RouteConfiguration.Separator + RouteConfiguration.ControllerName)
]
public sealed class TaskController : Controller
{
    [HttpGet("{task:guid}")]
    public IActionResult Index(Guid task) => View();
}