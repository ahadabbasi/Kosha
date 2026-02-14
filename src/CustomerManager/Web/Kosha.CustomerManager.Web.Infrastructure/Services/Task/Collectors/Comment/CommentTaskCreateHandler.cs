using System;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Handlers;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Task.Collectors.Comment;

internal sealed class CommentTaskCreateHandler : ITaskCreateHandler<CommentTaskCreateRequest>
{
    public ValueTask<Result<Guid>> Handle(CommentTaskCreateRequest request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(Result.Success(Guid.NewGuid()));
    }
}