using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Shared.Results;
using System;
using System.Threading;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task;

public interface ITaskManagerService
{
    /// <summary>
    /// Create a new task
    /// Find correct collector for task type and assign task to it
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    System.Threading.Tasks.Task<Result<Guid>> CreateAsync(CancellationToken cancellation);


    /// <summary>
    /// Paginate all tasks and return obligation of them
    /// Obligation is reviewed all them
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    System.Threading.Tasks.Task<Result<PaginateResponse<ITaskPaginateResponse>>> PaginateAsync(TaskPaginationRequest? request = null, CancellationToken cancellation = default);

    /// <summary>
    /// Change the category of the task
    /// </summary>
    /// <param name="task"></param>
    /// <param name="category"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    System.Threading.Tasks.Task<Result> ChangeCategoryAsync(Guid task, Guid category, CancellationToken cancellation);

    /// <summary>
    /// Get current category assign to the task
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    System.Threading.Tasks.Task<Result<Guid>> CategoryAsync(Guid task, CancellationToken cancellation);
}