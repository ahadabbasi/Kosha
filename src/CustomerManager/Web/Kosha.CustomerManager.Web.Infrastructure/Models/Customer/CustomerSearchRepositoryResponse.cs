using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Customer;

public sealed record CustomerSearchRepositoryResponse(
    Guid Id, 
    string Name, 
    string Family, 
    string FullName,
    DateTime Inserted
) : CustomerResponse(Id, Name, Family);