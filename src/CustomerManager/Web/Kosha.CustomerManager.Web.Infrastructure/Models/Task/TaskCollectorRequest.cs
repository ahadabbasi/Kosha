using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task;

public record TaskCollectorRequest(string Type) : ITaskCollectorRequest;