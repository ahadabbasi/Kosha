using Kosha.CustomerManager.Web.Models.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Controllers;

[Area(AreaNameConfiguration.Dashboard)]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}