using System;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;

public sealed record CommandTaskPaginateRequest(IEnumerable<Guid> Records) : TaskPaginateRequest(TaskTypeConfiguration.Comment, Records);