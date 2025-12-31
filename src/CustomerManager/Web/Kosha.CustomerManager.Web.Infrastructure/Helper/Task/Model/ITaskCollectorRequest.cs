using System;
using Kosha.CustomerManager.Web.Shared.Results;
using Mediator;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

public interface ITaskCollectorRequest : IRequest<Result<Guid>>;