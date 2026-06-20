using System;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task;

public record TaskDetailsRequest(string Type, Guid Record) : TaskCollectorRequest(Type), ITaskDetailsRequest;