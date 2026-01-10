using System.Collections;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Infrastructure.Models.Customer;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public sealed record ContactTypeResponse(
    string Name,
    string Title,
    string Selected,
    bool Disabled,
    IEnumerable<CustomerContactTypeResponse> Types
) : ContactTypeRequest(
    Name, 
    Title, 
    Selected,
    Disabled
), IEnumerable<CustomerContactTypeResponse>
{
    public IEnumerator<CustomerContactTypeResponse> GetEnumerator() => 
        Types.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();
}