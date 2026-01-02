using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Account.Models.ViewModels;
using Kosha.CustomerManager.Web.Areas.Dashboard.Controllers;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Extensions;
using Kosha.CustomerManager.Web.Shared.Models.Configurations;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Account.Controllers;

[Area(AreaNameConfiguration.Account)]
public sealed class LoginController(
    IUserService authenticationService
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

        Result<AuthenticationResponse> resultOfFind =
            await authenticationService.FindByUsernameAsync(
                new AuthenticationRequest(entry.Username)    
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
                    await authenticationService.VerifyPasswordAsync(
                        new AuthenticationVerifiedPasswordRequest(
                            resultOfFind,
                            entry.Password
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
                {
                    result = Redirect(entry.ReturnUrl);
                }

                IList<Claim> claims =
                    new List<Claim>
                    {
                        new(
                            ClaimDefinitionConfiguration.Identifier, 
                            resultOfFind.Data.Id.ToString()
                        ),
                        new(
                            ClaimDefinitionConfiguration.Username,
                            resultOfFind.Data.Username
                        ),
                        new(
                            ClaimDefinitionConfiguration.PhoneNumber,
                            resultOfFind.Data.PhoneNumber
                        )
                    };

                foreach ((string type, string value) in 
                        new Dictionary<string, string>
                        {
                            {
                                ClaimDefinitionConfiguration.Name,
                                resultOfFind.Data.Name
                            },
                            {
                                ClaimDefinitionConfiguration.Family,
                                resultOfFind.Data.Family
                            }
                        }
                    )
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        claims.Add(new Claim(type, value));
                    }
                }

                Result<IEnumerable<string>> resultOfRoles =
                    await authenticationService.RolesAsync(
                        resultOfFind
                    );

                IList<string> roles = new List<string>();

                if (
                    resultOfRoles &&
                    resultOfRoles.Data != null &&
                    resultOfRoles.Data.Any()
                )
                    foreach (string role in resultOfRoles.Data) 
                        roles.Add(role);

                if (!roles.Any(item => item.Equals(RoleNameConfiguration.User, StringComparison.OrdinalIgnoreCase))) 
                    roles.Add(RoleNameConfiguration.User);

                claims.Add(
                    new Claim(
                        ClaimDefinitionConfiguration.Role, 
                        string.Join(
                            ", ", 
                            roles
                        )
                    )
                );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(
                        [
                            new ClaimsIdentity(
                                claims,
                                CookieAuthenticationDefaults.AuthenticationScheme
                            )
                        ]
                    ),
                    new AuthenticationProperties { IsPersistent = entry.RememberMe }
                );
            }

        }


        return result;
    }
}