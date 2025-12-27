using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Account.Models.ViewModels;
using Kosha.CustomerManager.Web.Areas.Dashboard.Controllers;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Hasher.Algorithms;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Hasher.Default;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Extensions;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Account.Controllers;

[Area(AreaNameConfiguration.Account)]
public sealed class LoginController(
    IHasherService hasherService,
    IUserService authenticationService
) : Controller
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
    public async Task<IActionResult> Index([Bind] LoginVm entry)
    {
        IActionResult result = View(entry);

        Result<AuthenticationResponse> resultOfFind =
            await authenticationService.FindByUsernameAsync(entry);

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
                await hasherService.VerifyAsync(
                    new HasherRequest(entry.Password),
                    new HasherResponse(resultOfFind.Data.Password)
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
                {
                    result = Redirect(entry.ReturnUrl);
                }

                IList<Claim> claims =
                    new List<Claim>
                    {
                            new(ClaimDefinitionConfiguration.Identifier, resultOfFind.Data.Id.ToString()),
                            new (ClaimDefinitionConfiguration.Name, resultOfFind.Data.Name),
                            new (ClaimDefinitionConfiguration.Family, resultOfFind.Data.Family),
                    };

                Result<IEnumerable<string>> resultOfRoles =
                    await authenticationService.RolesAsync(
                        entry
                    );

                if (
                    resultOfRoles &&
                    resultOfRoles.Data != null &&
                    resultOfRoles.Data.Any()
                )
                {
                    claims.Add(
                        new Claim(ClaimDefinitionConfiguration.Role, string.Join(", ", resultOfRoles.Data))
                    );
                }

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(new ClaimsIdentity(claims)),
                    new AuthenticationProperties
                    {
                        IsPersistent = entry.RememberMe
                    }
                );
            }

        }


        return result;
    }
}