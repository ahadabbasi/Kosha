using System;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Customer;
using Kosha.CustomerManager.Web.Infrastructure.Models.Customer;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Extensions;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Controllers;

[
    Area(AreaNameConfiguration.Dashboard),
    Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme),
    Route(
        RouteConfiguration.Separator +
        RouteConfiguration.AreaName +
        RouteConfiguration.Separator +
        RouteConfiguration.ControllerName +
        RouteConfiguration.Separator +
        "{customer:guid}"
    )
]
public sealed class ContactController(
    ICustomerService customerService
) : Controller
{
    [HttpGet(RouteConfiguration.ActionName)]
    public async Task<IActionResult> List(Guid customer)
    {
        Result<CustomerContactListResponse> result =
            await customerService.ListOfCustomerContactAsync(customer);

        return
            result ?
                View(result.Data) :
                CustomerList();
    }

    [HttpGet(RouteConfiguration.ActionName)]
    public async Task<IActionResult> New(Guid customer)
    {
        Result<CustomerResponse> resultCustomer =
            await customerService.FindByIdAsync(customer);
        return
            resultCustomer && resultCustomer.Data is not null ?
                View(
                    new NewContactVm(resultCustomer.Data)
                ) :
                CustomerList();
    }

    [
        HttpPost(RouteConfiguration.ActionName),
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> New(Guid customer, [Bind] NewContactVm entry)
    {
        IActionResult result =
            View(entry);

        if (ModelState.IsValid)
        {
            if (customer == entry.Customer)
            {
                ModelState.AddError(
                    await customerService.AddNewContactToCustomerAsync(
                        customer,
                        new CustomerContactRequest(
                            entry.Type,
                            entry.Value
                        )
                    )
                );

                if (ModelState.IsValid)
                    result =
                        RedirectToAction(
                            nameof(List),
                            nameof(ContactController).RemoveControllerFromString(),
                            new { Area = AreaNameConfiguration.Dashboard, customer }
                        );
            }
        }

        return result;
    }

    [
        HttpGet(
            RouteConfiguration.ActionName +
            RouteConfiguration.Separator +
            "{id:guid}"
        )
    ]
    public Task<IActionResult> Update(Guid customer, Guid id) =>
        FindCustomerContact(
            customer,
            id,
            nameof(Update)
        );

    [
        HttpPost(
            RouteConfiguration.ActionName +
            RouteConfiguration.Separator +
            "{id:guid}"
        ),
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Update(Guid customer, Guid id, [Bind] EditContactVm request)
    {
        IActionResult result =
            View(request);

        if (ModelState.IsValid)
        {
            if (request.Customer == customer)
            {
                if (request.Id == id)
                {
                    ModelState.AddError(
                        await customerService.UpdateContactOfCustomerAsync(
                            customer,
                            id,
                            new CustomerContactRequest(request.Type, request.Value)
                        )
                    );

                    if (ModelState.IsValid)
                        result =
                            RedirectToAction(
                                nameof(List),
                                nameof(ContactController).RemoveControllerFromString(),
                                new { Area = AreaNameConfiguration.Dashboard, customer }
                            );

                }
            }
        }

        return result;
    }

    [
        HttpGet(
            RouteConfiguration.ActionName +
            RouteConfiguration.Separator +
            "{id:guid}"
        )
    ]
    public Task<IActionResult> Delete(Guid customer, Guid id) =>
        FindCustomerContact(
            customer,
            id,
            nameof(Delete)
        );

    [
        HttpPost(
            RouteConfiguration.ActionName +
            RouteConfiguration.Separator +
            "{id:guid}"
        ),
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Delete(Guid customer, Guid id, [Bind] EditContactVm request)
    {
        IActionResult result =
            View(request);

        if (ModelState.IsValid)
        {
            if (request.Customer == customer)
            {
                if (request.Id == id)
                {
                    ModelState.AddError(
                        await customerService.RemoveContactFromCustomerAsync(
                            customer,
                            id
                        )
                    );

                    if (ModelState.IsValid)
                        result =
                            RedirectToAction(
                                nameof(List),
                                nameof(ContactController).RemoveControllerFromString(),
                                new { Area = AreaNameConfiguration.Dashboard, customer }
                            );

                }
            }
        }

        return result;
    }

    private async Task<IActionResult> FindCustomerContact(Guid customer, Guid contact, string viewName)
    {
        IActionResult result =
            CustomerList();

        Result<CustomerResponse> resultCustomer =
            await customerService.FindByIdAsync(customer);

        if (resultCustomer && resultCustomer.Data is not null)
        {
            result =
                RedirectToAction(
                    nameof(List),
                    nameof(ContactController).RemoveControllerFromString(),
                    new { Area = AreaNameConfiguration.Dashboard, Customer = customer }
                );

            Result <CustomerContactResponse> resultContact =
                await customerService.FindContactOfUser(customer, contact);

            if (resultContact && resultContact.Data is not null)
                result =
                    View(
                        viewName,
                        new EditContactVm(resultCustomer.Data, resultContact.Data)
                    );
        }

        return result;
    }

    private IActionResult CustomerList() =>
        RedirectToAction(
            nameof(CustomerController.List),
            nameof(CustomerController).RemoveControllerFromString(),
            new { Area = AreaNameConfiguration.Dashboard }
        );
}