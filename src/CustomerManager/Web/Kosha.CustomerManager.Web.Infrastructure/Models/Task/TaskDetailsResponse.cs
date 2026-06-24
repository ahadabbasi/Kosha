using System.Collections.Generic;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task;

internal sealed record TaskDetailsResponse(
    string Title, 
    IEnumerable<ITaskDetailsInformationResponse>? Information
) : ITaskDetailsResponse;