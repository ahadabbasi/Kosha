using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;

public sealed record ContactTaskRepositoryResponse(
    Guid Id, string? Organization, 
    string? Description, DateTime Inserted
);