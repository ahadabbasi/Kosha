using System;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Tag;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Infrastructure.Models.Tag;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Extensions;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Controllers;

[
    Area(AreaNameConfiguration.Dashboard),
    Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)
]
public class TagController(ITagService tagService) : Controller
{
    public async Task<IActionResult> List([Bind] PaginateRequest? request = null) =>
        View((await tagService.PaginateAsync(request)).Data);

    public IActionResult New() =>
        View();

    [
        HttpPost,
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> New([Bind] NewTagVm entry)
    {
        IActionResult result = View(entry);

        if (ModelState.IsValid)
        {
            ModelState.AddError(
                await tagService.CreateAsync(
                    new TagRequest(entry.Title)
                )
            );

            if (ModelState.IsValid)
                result =
                    RedirectToAction(
                        nameof(List),
                        nameof(TagController).RemoveControllerFromString(),
                        new { Area = AreaNameConfiguration.Dashboard }
                    );
        }

        return result;
    }

    public Task<IActionResult> Edit(Guid id) 
        => FindByIdAsync(id, nameof(Edit));

    [
        HttpPost,
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Edit(Guid id, [Bind] TagVm entry)
    {
        IActionResult result = View(entry);

        if (ModelState.IsValid)
        {
            ModelState.AddError(
                new Error(
                    nameof(ErrorMessageConfiguration.DoNotChangeValue), 
                    ErrorMessageConfiguration.DoNotChangeValue
                )
            );

            if (id == entry.Id)
            {
                ModelState.Clear();

                ModelState.AddError(
                    await tagService.UpdateAsync(
                        id, 
                        new TagRequest(entry.Title)
                    )
                );

                if (ModelState.IsValid)
                    result =
                        RedirectToAction(
                            nameof(List),
                            nameof(TagController).RemoveControllerFromString(),
                            new { Area = AreaNameConfiguration.Dashboard }
                        );
            }
        }

        return result;
    }

    [
        HttpPost,
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Delete(Guid id, [Bind] TagVm entry)
    {
        IActionResult result = View(entry);

        if (ModelState.IsValid)
        {
            ModelState.AddError(
                new Error(
                    nameof(ErrorMessageConfiguration.DoNotChangeValue),
                    ErrorMessageConfiguration.DoNotChangeValue
                )
            );

            if (id == entry.Id)
            {
                ModelState.Clear();

                ModelState.AddError(await tagService.DeleteAsync(id));

                if (ModelState.IsValid)
                    result =
                        RedirectToAction(
                            nameof(List),
                            nameof(TagController).RemoveControllerFromString(),
                            new { Area = AreaNameConfiguration.Dashboard }
                        );
            }
        }

        return result;
    }


    public Task<IActionResult> Delete(Guid id)
        => FindByIdAsync(id, nameof(Delete));


    private async Task<IActionResult> FindByIdAsync(Guid id, string viewName)
    {
        IActionResult result =
            RedirectToAction(
                nameof(List),
                nameof(TagController).RemoveControllerFromString(),
                new { Area = AreaNameConfiguration.Dashboard }
            );

        Result<TagResponse> resultTag = await tagService.FindByIdAsync(id);

        if (resultTag && resultTag.Data is not null)
            result =
                View(
                    viewName,
                    new TagVm(resultTag.Data)
                );

        return result;
    }
}