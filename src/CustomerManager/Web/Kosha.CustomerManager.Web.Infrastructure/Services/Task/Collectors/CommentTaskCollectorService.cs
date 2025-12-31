using System;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Task.Collectors;

internal sealed class CommentTaskCollectorService : ITaskCollectorService<CommentTaskCollectorRequest>
{
    public ValueTask<Result<Guid>> Handle(CommentTaskCollectorRequest request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(Result.Success(Guid.NewGuid()));
    }
}