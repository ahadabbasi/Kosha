using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Customer;

public sealed record CustomerContactResponse(
    Guid Id,
    string Type,
    string Value
) : CustomerContactRequest(Type, Value);