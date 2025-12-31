using System;
using System.Threading;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task;

public interface ITaskManagerService
{
    /// <summary>
    /// Create a new task
    /// Find correct collector for task type and assign task to it
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    System.Threading.Tasks.Task<Result<Guid>> CreateAsync(TaskCreateRequest entry, CancellationToken cancellation);
}