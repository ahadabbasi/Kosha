using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task;

internal sealed record TaskDetailsInformationResponse(
    string Key, string? Value
) : ITaskDetailsInformationResponse;