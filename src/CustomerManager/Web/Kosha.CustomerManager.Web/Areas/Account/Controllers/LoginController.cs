using Kosha.CustomerManager.Web.Areas.Account.Models.ViewModels;
using Kosha.CustomerManager.Web.Models.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Account.Controllers;

[Area(AreaNameConfiguration.Account)]
public sealed class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index(string? returnUrl = null) 
        => View(
            new LoginVm(
                string.Empty, 
                string.Empty, 
                false, 
                returnUrl
            )
        );

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Index([Bind] LoginVm entry)
    {
        IActionResult result = View(entry);

        if (ModelState.IsValid)
        {
        }

        return result;
    }
}