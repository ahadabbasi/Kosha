using System;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Shared.Results;
using Mediator;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task;

public interface ITaskCollectorService<TRequest> : IRequestHandler<TRequest, Result<Guid>>
    where TRequest : class, ITaskCollectorRequest
{
}