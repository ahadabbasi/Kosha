using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Models.Action;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Action;

public interface IActionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<IEnumerable<ActionResponse>>> FetchTaskActionsAsync(Guid task, CancellationToken cancellation = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> AppendActionToTaskAsync(Guid task, ActionRequest request, CancellationToken cancellation = default);
}