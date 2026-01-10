using System;
using System.Collections;
using System.Collections.Generic;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Customer;

public sealed record CustomerContactListResponse(
    Guid Id,
    string Name,
    string Family,
    IEnumerable<CustomerContactResponse>? Contacts = null
) : IEnumerable<CustomerContactResponse>
{
    public IEnumerator<CustomerContactResponse> GetEnumerator() => 
        (Contacts ?? []).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();
}