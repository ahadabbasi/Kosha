using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Account.Models.ViewModels;
using Kosha.CustomerManager.Web.Areas.Dashboard.Controllers;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Extensions;
using Kosha.CustomerManager.Web.Models.Infrastructure.Helper;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Account.Controllers;

[Area(AreaNameConfiguration.Account)]
public sealed class LoginController(
    IUserService userService,
    ICookieAuthenticationMethodService authenticationMethodService
) : Controller
{
    [HttpGet]
    public IActionResult Index(string? returnUrl = null)=> 
        View(
            new LoginVm
            {
                ReturnUrl = returnUrl
            }
        );

    [
        HttpPost, 
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Index([Bind] LoginVm entry)
    {
        IActionResult result = View(entry);

        if (ModelState.IsValid)
        {

            Result<UserResponse> resultOfFind =
                await userService.FindByUsernameAsync(
                    new UserRequest(entry.Username)
                );

            ModelState.AddError(ErrorConfiguration.UsernameOrPasswordIsWrong);

            if (!resultOfFind)
            {
                ModelState.Clear();
                ModelState.AddError(resultOfFind);
            }

            if (
                resultOfFind &&
                resultOfFind.Data != null
            )
            {
                Result resultOfVerified =
                    await authenticationMethodService.SignInAsync(
                        new AuthenticationSignInRequest(
                            resultOfFind,
                            entry.Password,
                            entry.RememberMe
                        )
                    );

                if (resultOfVerified)
                {
                    ModelState.Clear();

                    result =
                        RedirectToAction(
                            nameof(HomeController.Index),
                            nameof(HomeController).RemoveControllerFromString(),
                            new { area = AreaNameConfiguration.Dashboard }
                        );

                    if (
                        !string.IsNullOrWhiteSpace(entry.ReturnUrl) &&
                        Url.IsLocalUrl(entry.ReturnUrl)
                    ) 
                        result = Redirect(entry.ReturnUrl);
                }

            }
        }

        return result;
    }
}