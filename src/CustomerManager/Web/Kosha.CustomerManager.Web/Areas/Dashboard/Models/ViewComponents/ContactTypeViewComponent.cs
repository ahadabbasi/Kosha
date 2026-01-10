using System.Collections.Generic;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Customer;
using Kosha.CustomerManager.Web.Infrastructure.Models.Customer;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewComponents;

public sealed class ContactTypeViewComponent(ICustomerService customerService): ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(ContactTypeRequest request)
    {
        Result<IEnumerable<CustomerContactTypeResponse>> resultTypes =
            await customerService.AcceptableContactTypesAsync();

        return
            resultTypes && resultTypes.Data is not null ? 
                View(
                    new ContactTypeResponse(
                        request.Name, 
                        request.Title,
                        request.Selected,
                        request.Disabled,
                        resultTypes.Data
                    )
                ) :
                Content(string.Empty);
    }
}