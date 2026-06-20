using System;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Shared.Results;
using Mediator;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

public interface ITaskDetailsRequest : ITaskCollectorRequest, IQuery<Result<IEnumerable<ITaskDetailsResponse>>
{
    /// <summary>
    /// the record need to received
    /// </summary>
    Guid Record { get; }
}