using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Shared.Results;
using Mediator;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Handlers;

public interface ITaskDetailsHandler<TDetailsQuery> : IQueryHandler<TDetailsQuery, Result<ITaskDetailsResponse>>
    where TDetailsQuery : class, ITaskDetailsRequest;