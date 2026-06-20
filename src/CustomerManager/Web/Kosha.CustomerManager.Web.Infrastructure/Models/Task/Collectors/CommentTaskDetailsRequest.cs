using System;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;

internal sealed record CommentTaskDetailsRequest(Guid Record) :
    TaskDetailsRequest(TaskTypeConfiguration.Comment, Record);