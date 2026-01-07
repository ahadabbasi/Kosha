using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Customer;

public sealed record CustomerPaginateResponse(
    Guid Id, 
    string Name, 
    string Family
) : CustomerRequest(Name, Family);