using Kosha.CustomerManager.Web.Models.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Controllers;

[
    Area(AreaNameConfiguration.Dashboard), 
    Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)
]
public class HomeController : Controller
{
    public IActionResult Index() 
        => View();
}