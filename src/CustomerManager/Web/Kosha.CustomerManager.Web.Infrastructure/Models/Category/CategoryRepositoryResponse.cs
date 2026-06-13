using System;
using Kosha.CustomerManager.Web.Domain.Enums;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Category;

internal sealed record CategoryRepositoryResponse(
    Guid Id, 
    string Name, 
    AnsEnum IsDefault, 
    DateTime Inserted
);