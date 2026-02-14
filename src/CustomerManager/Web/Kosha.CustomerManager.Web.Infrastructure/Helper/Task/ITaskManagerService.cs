using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Shared.Results;
using System;
using System.Collections.Generic;
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
    System.Threading.Tasks.Task<Result<PaginateResponse<ITaskPaginateResponse>>> PaginateAsync(PaginateRequest? request = null, CancellationToken cancellation = default);
}