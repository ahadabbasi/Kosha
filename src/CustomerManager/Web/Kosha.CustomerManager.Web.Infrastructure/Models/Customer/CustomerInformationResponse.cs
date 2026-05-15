using System;
using System.Collections.Generic;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Customer;

public sealed record CustomerInformationResponse(
    Guid Id,
    string Name,
    string Family,
    IEnumerable<CustomerContactResponse>? Contacts = null
);