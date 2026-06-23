using System;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;

internal sealed record ContactTaskDetailsRequest(Guid Record) :
    TaskDetailsRequest(TaskTypeConfiguration.Contact, Record);