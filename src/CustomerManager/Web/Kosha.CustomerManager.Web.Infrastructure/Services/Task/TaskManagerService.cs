using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Shared.Results;
using Mediator;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Task;

internal sealed class TaskManagerService(
    IMediator mediator,
    ITaskBinderService binderService,
    PaginateHelperService paginateHelperService
) : ITaskManagerService
{
    public async Task<Result<Guid>> CreateAsync(CancellationToken cancellation)
    {
        Result<Guid> result = 
            Result.Failed<Guid>(
                ErrorConfiguration.TaskTypeInvalid
            );

        try
        {
            Result<ITaskCreateRequest> resultRequest = await binderService.BindCreateAsync(cancellation);

            if(
                resultRequest &&
                resultRequest.Data is not null
            ) 
                result = await mediator.Send(resultRequest.Data, cancellation);
        }
        catch (Exception )
        {
            //
        }

        return result;
    }

    public async Task<Result<PaginateResponse<ITaskPaginateResponse>>> PaginateAsync(PaginateRequest? request = null, CancellationToken cancellation = default)
    {
        Result<PaginateResponse<ITaskPaginateResponse>> result =
            Result.Failed<PaginateResponse<ITaskPaginateResponse>>(
                ErrorConfiguration.TaskTypeInvalid
            );

        string[] records =
            [
                "bc922e6f-5105-4304-b2ef-6756212f659f",
                "a81e272d-3c56-4507-9dd1-7f1d1e086a10",
                "e1119b09-d487-4a8a-939b-bbba02009636",
                "f9da8be2-a7e8-4ac5-a5ae-a94473d627cf",
                "fecb80b6-4b16-4e9e-ac67-97ca834bebf8",
                "de54db63-e838-42ed-874a-69e901dbd2a0",
                "c8687ca7-ae33-4501-ae67-8235260530e1",
                "9c8e6330-4c0f-48a8-8ee7-f9687f914835",
                "38102a6d-2022-43d2-affd-987da6128d2e",
                "c4c3a7cf-a597-420e-bcde-7f8bedf09598"
            ];

        Result<ITaskPaginateRequest> resultRequest = 
            await binderService.BindPaginateAsync(
                new TaskCollectorRequest(TaskTypeConfiguration.Comment), 
                records.Select(Guid.Parse), 
                cancellation
            );

        ITaskPaginateResponse[] data = [];

        if (
            resultRequest &&
            resultRequest.Data is not null
        )
        {
            Result<IEnumerable<ITaskPaginateResponse>> resultPaginate = 
                await mediator.Send(resultRequest.Data, cancellation);

            if (resultPaginate && resultPaginate.Data is not null)
                data = data.Concat(resultPaginate.Data).ToArray();
        }

        return Result.Success(new PaginateResponse<ITaskPaginateResponse>(data, data.Length, 1, 1, data.Length));
    }
}