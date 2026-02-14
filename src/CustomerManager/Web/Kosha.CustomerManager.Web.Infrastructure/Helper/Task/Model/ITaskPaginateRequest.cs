using System;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Shared.Results;
using Mediator;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

public interface ITaskPaginateRequest : ITaskCollectorRequest, IEnumerable<Guid>, IQuery<Result<IEnumerable<ITaskPaginateResponse>>>
{
    /// <summary>
    /// All the records need to received
    /// </summary>
    IEnumerable<Guid> Records { get; }
}