using System;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;

internal sealed record ContactTaskPaginateRequest(IEnumerable<Guid> Records) :
    TaskPaginateRequest(TaskTypeConfiguration.Contact, Records);