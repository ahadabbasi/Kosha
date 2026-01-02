using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Controllers;

[
    Area(AreaNameConfiguration.Dashboard),
    Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)
]
public class AccountController(IUserService userService) : Controller
{
    public async Task<IActionResult> List([Bind] PaginateRequest? request = null) => 
        View(await userService.PaginateAsync(request));

    public IActionResult Create() 
        => View();

    [
        HttpPost, 
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Create([Bind]CreateUserVm entry)
    {
        IActionResult result = View(entry);

        if (ModelState.IsValid)
        {
            ModelState.AddError(
                await userService.SaveAsync(
                    new AuthenticationSaveRequest(
                        entry.Username,
                        entry.Password,
                        entry.Name,
                        entry.Family,
                        entry.PhoneNumber
                    )
                )
            );

            if (ModelState.IsValid)
                result =
                    RedirectToAction(
                        nameof(List),
                        nameof(AccountController).RemoveControllerFromString(),
                        new { Area = AreaNameConfiguration.Dashboard }
                    );
        }

        return result;
    }
}