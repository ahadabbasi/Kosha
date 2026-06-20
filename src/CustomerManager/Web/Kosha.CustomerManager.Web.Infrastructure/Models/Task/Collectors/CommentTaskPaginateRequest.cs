using System;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;

internal sealed record CommentTaskPaginateRequest(IEnumerable<Guid> Records) :
    TaskPaginateRequest(TaskTypeConfiguration.Comment, Records);