using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Controllers;

public sealed class HomeController : Controller
{
    public IActionResult Index() => 
        RedirectToAction(
            nameof(Areas.Dashboard.Controllers.HomeController.Index), 
            nameof(Areas.Dashboard.Controllers.HomeController).RemoveControllerFromString(),
            new
            {
                Area = AreaNameConfiguration.Dashboard
            }
        );
}