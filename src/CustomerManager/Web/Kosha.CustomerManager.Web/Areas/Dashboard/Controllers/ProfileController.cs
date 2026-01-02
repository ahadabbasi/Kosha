using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
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
public sealed class ProfileController(IUserService userService) : Controller
{
    public IActionResult Index()
    {
        ProfileUpdateVm model = new ProfileUpdateVm();

        foreach ((string claimType, PropertyInfo? property)in 
                new Dictionary<string, PropertyInfo?>
                {
                    {
                        ClaimDefinitionConfiguration.Username,
                        typeof(ProfileUpdateVm).GetProperty(nameof(ProfileUpdateVm.Username))
                    },
                    {
                        ClaimDefinitionConfiguration.Name,
                        typeof(ProfileUpdateVm).GetProperty(nameof(ProfileUpdateVm.Name))
                    },
                    {
                        ClaimDefinitionConfiguration.Family,
                        typeof(ProfileUpdateVm).GetProperty(nameof(ProfileUpdateVm.Family))
                    },
                    {
                        ClaimDefinitionConfiguration.PhoneNumber,
                        typeof(ProfileUpdateVm).GetProperty(nameof(ProfileUpdateVm.PhoneNumber))
                    }
                }
            )
        {
            if(
                property is null || 
                !property.CanWrite
            )
                continue;

            Claim? claim = User.FindFirst(claimType);

            if(claim is null)
                continue;

            string value = claim.Value;

            if(string.IsNullOrEmpty(value))
                continue;

            try
            {
                Type propertyType = property.PropertyType;

                if (
                    propertyType.IsGenericType && 
                    propertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                )
                {
                    propertyType = propertyType.GetGenericArguments().First();
                }
            
                property.SetValue(
                    model,
                    Convert.ChangeType(
                        value,
                        propertyType
                    )
                );

            }
            catch
            {
                //
            }
        }

        return View(model);
    }

    [
        HttpPost,
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Index([Bind] ProfileUpdateVm entry)
    {
        IActionResult result = View(entry);

        if (ModelState.IsValid)
        {
            ModelState.AddError(
                await userService.UpdateAsync(
                    Guid.Parse(
                        User.FindFirstValue(ClaimDefinitionConfiguration.Identifier) ?? Guid.Empty.ToString()
                    ),
                    new AuthenticationUpdateRequest(
                        entry.Username,
                        entry.Name,
                        entry.Family,
                        entry.PhoneNumber
                    )
                )
            );

            if (ModelState.IsValid)
                result =
                    RedirectToAction(
                        nameof(HomeController.Index),
                        nameof(HomeController).RemoveControllerFromString(),
                        new { Area = AreaNameConfiguration.Dashboard }
                    );
        }

        return result;
    }

    public IActionResult ChangePassword()
        => View();

    [
        HttpPost,
        ValidateAntiForgeryToken
    ]
    public IActionResult ChangePassword([Bind] ChangePasswordVm entry)
    {
        return View();
    }
}