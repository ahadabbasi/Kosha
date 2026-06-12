using System.Diagnostics;
using Kosha.CustomerManager.Web.Models;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kosha.CustomerManager.Web.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
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

    public IActionResult Privacy()
        => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}