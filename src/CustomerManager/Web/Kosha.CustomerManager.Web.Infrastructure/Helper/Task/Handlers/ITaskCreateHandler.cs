using System;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Shared.Results;
using Mediator;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Handlers;

public interface ITaskCreateHandler<TCreateCommand> : ICommandHandler<TCreateCommand, Result<Guid>>  
    where TCreateCommand : class, ITaskCreateRequest;