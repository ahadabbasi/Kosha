using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Account.Controllers;

[
    Authorize, 
    Area(AreaNameConfiguration.Account)
]
public sealed class LogoutController : Controller
{
    public async Task<IActionResult> Index()
    {
        await HttpContext.SignOutAsync();

        return
            RedirectToAction(
                nameof(LoginController.Index),
                nameof(LoginController).RemoveControllerFromString(), 
                new {Area = AreaNameConfiguration.Account}
            );
    }
}