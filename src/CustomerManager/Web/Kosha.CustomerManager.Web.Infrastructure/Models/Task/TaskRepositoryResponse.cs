using System;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task;

internal sealed record TaskRepositoryResponse(string Type, Guid Id);