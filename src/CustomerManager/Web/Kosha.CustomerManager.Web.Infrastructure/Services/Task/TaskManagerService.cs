using System;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Shared.Results;
using Mediator;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Task;

internal sealed class TaskManagerService(
    IMediator mediator
) : ITaskManagerService
{
    public async Task<Result<Guid>> CreateAsync(TaskCreateRequest entry, CancellationToken cancellation)
    {
        Result<Guid> result = 
            Result.Failed<Guid>(
                ErrorConfiguration.TaskTypeInvalid
            );

        try
        {
            result = await mediator.Send(entry, cancellation);
        }
        catch (Exception )
        {
            //
        }

        return result;
    }
}