using System.Collections.Generic;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Shared.Results;
using Mediator;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Handlers;

public interface ITaskPaginateHandler<TPaginateQuery> : IQueryHandler<TPaginateQuery, Result<IEnumerable<ITaskPaginateResponse>>>
    where TPaginateQuery : class, ITaskPaginateRequest;