using System;
using Kosha.CustomerManager.Web.Shared.Results;
using Mediator;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;

public interface ITaskCreateRequest : ITaskCollectorRequest, ICommand<Result<Guid>>;