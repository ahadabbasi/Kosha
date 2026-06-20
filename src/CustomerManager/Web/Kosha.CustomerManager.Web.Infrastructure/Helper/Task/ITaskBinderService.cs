using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task;

public interface ITaskBinderService
{
    /// <summary>
    /// Bind data and try to create valid request for creation task
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<ITaskCreateRequest>> BindCreateAsync(CancellationToken cancellation = default);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="records"></param>
    /// <param name="cancellation"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Result<ITaskPaginateRequest>> BindPaginateAsync(string request, IEnumerable<Guid> records, CancellationToken cancellation = default);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="record"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<ITaskDetailsRequest>> BindDetailsAsync(string request, Guid record, CancellationToken cancellation = default);
}