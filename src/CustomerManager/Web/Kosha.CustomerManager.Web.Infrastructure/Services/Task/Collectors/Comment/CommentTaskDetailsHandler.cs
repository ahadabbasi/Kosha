using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Handlers;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Task.Collectors.Comment;

internal sealed class CommentTaskDetailsHandler : ITaskDetailsHandler<CommentTaskDetailsRequest>
{
    public ValueTask<Result<IEnumerable<ITaskDetailsResponse>>> Handle(CommentTaskDetailsRequest query, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}