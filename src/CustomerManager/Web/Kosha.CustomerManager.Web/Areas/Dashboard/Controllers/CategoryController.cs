using System;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Category;
using Kosha.CustomerManager.Web.Infrastructure.Models.Category;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
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
public sealed class CategoryController(ICategoryService categoryService) : Controller
{
    public async Task<IActionResult> List([Bind]PaginateRequest? request) => 
        View((await categoryService.PaginateAsync(request)).Data);

    [HttpGet]
    public IActionResult New() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> New([Bind] NewTaxonomyVm entry)
    {
        IActionResult result = View(entry);

        if (ModelState.IsValid)
        {
            ModelState.AddError(
                await categoryService.CreateAsync(
                    new CategoryRequest(entry.Title)
                )
            );

            if (ModelState.IsValid)
                result =
                    RedirectToAction(
                        nameof(List),
                        nameof(CategoryController).RemoveControllerFromString(),
                        new { Area = AreaNameConfiguration.Dashboard }
                    );
        }

        return result;
    }

    [HttpGet]
    public Task<IActionResult> Edit(Guid id)
        => FindByIdAsync(id, nameof(Edit));

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind] TaxonomyVm entry)
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
                    await categoryService.UpdateAsync(
                        id,
                        new CategoryRequest(entry.Title)
                    )
                );

                if (ModelState.IsValid)
                    result =
                        RedirectToAction(
                            nameof(List),
                            nameof(CategoryController).RemoveControllerFromString(),
                            new { Area = AreaNameConfiguration.Dashboard }
                        );
            }
        }

        return result;
    }

    [HttpGet]
    public Task<IActionResult> Delete(Guid id)
        => FindByIdAsync(id, nameof(Delete));

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, [Bind] TaxonomyVm entry)
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

                ModelState.AddError(await categoryService.DeleteAsync(id));

                if (ModelState.IsValid)
                    result =
                        RedirectToAction(
                            nameof(List),
                            nameof(CategoryController).RemoveControllerFromString(),
                            new { Area = AreaNameConfiguration.Dashboard }
                        );
            }
        }

        return result;
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> MakeDefault(Guid id)
    {
        await categoryService.MakeDefaultAsync(id);

        return 
            RedirectToAction(
                nameof(List),
                nameof(CategoryController).RemoveControllerFromString(),
                new { Area = AreaNameConfiguration.Dashboard }
            );
    }

    private async Task<IActionResult> FindByIdAsync(Guid id, string viewName)
    {
        IActionResult result =
            RedirectToAction(
                nameof(List),
                nameof(CategoryController).RemoveControllerFromString(),
                new { Area = AreaNameConfiguration.Dashboard }
            );

        Result<CategoryResponse> resultTag = await categoryService.FindByIdAsync(id);

        if (resultTag && resultTag.Data is not null)
            result =
                View(
                    viewName,
                    new TaxonomyVm(resultTag.Data)
                );

        return result;
    }
}