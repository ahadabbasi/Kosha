using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Task;

public interface ITaskBinderService
{
    Task<Result<TRequest>> BindAsync<TRequest>(CancellationToken cancellation = default)
        where TRequest : class, ITaskCollectorRequest;
}