using System;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Customer;
using Kosha.CustomerManager.Web.Infrastructure.Models.Customer;
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
public sealed class CustomerController(
    ICustomerService customerService
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> List(
        [Bind]PaginateRequest? request = null
    ) =>
        View(
            (await customerService.PaginateAsync(request)).Data
        );

    [HttpGet]
    public IActionResult Create() =>
        View();


    [
        HttpPost, 
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Create([Bind] CreateCustomerVm request)
    {
        IActionResult result = View(request);

        if (ModelState.IsValid)
        {
            ModelState.AddError(
                await customerService.CreateAsync(
                    new CustomerRequest(request.Name, request.Family)
                )
            );

            if (ModelState.IsValid)
                result =
                    RedirectToAction(
                        nameof(List),
                        nameof(CustomerController).RemoveControllerFromString(),
                        new { Area = AreaNameConfiguration.Dashboard }
                    );
        }

        return result;
    }

    [HttpGet]
    public Task<IActionResult> Edit(Guid id) =>
        FindByIdAsync(id, nameof(Edit));

    [
        HttpPost,
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Edit(Guid id, [Bind] UpdateCustomerVm entry)
    {
        IActionResult result = View(entry);

        if (ModelState.IsValid && entry.Id == id)
        {
            ModelState.AddError(
                await customerService.UpdateAsync(
                    id, 
                    new CustomerRequest(
                        entry.Name, 
                        entry.Family
                    )
                )
            );

            if (ModelState.IsValid)
                result = 
                    RedirectToAction(
                        nameof(List), 
                        nameof(CustomerController).RemoveControllerFromString(),
                        new { Area = AreaNameConfiguration.Dashboard }
                    );
        }

        return result;
    }

    [HttpGet]
    public Task<IActionResult> Delete(Guid id) =>
        FindByIdAsync(id, nameof(Delete));

    [
        HttpPost,
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Delete(Guid id, [Bind] UpdateCustomerVm entry)
    {
        IActionResult result = View(entry);

        if (ModelState.IsValid && entry.Id == id)
        {
            ModelState.AddError(
                await customerService.DeleteAsync(
                    id
                )
            );

            if (ModelState.IsValid)
                result =
                    RedirectToAction(
                        nameof(List),
                        nameof(CustomerController).RemoveControllerFromString(),
                        new { Area = AreaNameConfiguration.Dashboard }
                    );
        }

        return result;
    }


    private async Task<IActionResult> FindByIdAsync(Guid id, string viewName)
    {
        IActionResult result =
            RedirectToAction(
                nameof(List),
                nameof(TagController).RemoveControllerFromString(),
                new { Area = AreaNameConfiguration.Dashboard }
            );

        Result<CustomerResponse> resultCustomer = await customerService.FindByIdAsync(id);

        if (resultCustomer && resultCustomer.Data is not null)
            result =
                View(
                    viewName,
                    new UpdateCustomerVm(resultCustomer.Data)
                );

        return result;
    }
}