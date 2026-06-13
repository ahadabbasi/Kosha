using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Extensions;
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

            if (resultRequest && resultRequest.Data is not null)
                result = await mediator.Send(resultRequest.Data, cancellation);
        }
        catch (Exception)
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

        KeyValuePair<int, string>[] records =
            [
                new(1, "3a9b8c7d-1e2f-4a3b-8c9d-0e1f2a3b4c5d"), 
                new(2, "4b2c9d8e-2f3a-5b4c-9d0e-1f2a3b4c5d6e"), 
                new(3, "5c3d0e9f-3a4b-6c5d-0e1f-2a3b4c5d6e7f"), 
                new(4, "6d4e1f0a-4b5c-7d6e-1f2a-3b4c5d6e7f8a"), 
                new(5, "7e5f2a1b-5c6d-8e7f-2a3b-4c5d6e7f8a9b"), 
                new(6, "8f6a3b2c-6d7e-9f8a-3b4c-5d6e7f8a9b0c"), 
                new(7, "9a7b4c3d-7e8f-0a9b-4c5d-6e7f8a9b0c1d"), 
                new(8, "0b8c5d4e-8f9a-1b0c-5d6e-7f8a9b0c1d2e"), 
                new(9, "1c9d6e5f-9a0b-2c1d-6e7f-8a9b0c1d2e3f"), 
                new(10, "2d0e7f6a-0b1c-3d2e-7f8a-9b0c1d2e3f4a"), 
                new(11, "3e1f8a7b-1c2d-4e3f-8a9b-0c1d2e3f4a5b"), 
                new(12, "4f2a9b8c-2d3e-5f4a-9b0c-1d2e3f4a5b6c"), 
                new(13, "5a3b0c9d-3e4f-6a5b-0c1d-2e3f4a5b6c7d"), 
                new(14, "6b4c1d0e-4f5a-7b6c-1d2e-3f4a5b6c7d8e"),
                new(15, "7c5d2e1f-5a6b-8c7d-2e3f-4a5b6c7d8e9f"), 
                new(16, "8d6e3f2a-6b7c-9d8e-3f4a-5b6c7d8e9f0a"), 
                new(17, "9e7f4a3b-7c8d-0e9f-4a5b-6c7d8e9f0a1b"), 
                new(18, "0f8a5b4c-8d9e-1f0a-5b6c-7d8e9f0a1b2c"), 
                new(19, "1a9b6c5d-9e0f-2a1b-6c7d-8e9f0a1b2c3d"), 
                new(20, "2b0c7d6e-0f1a-3b2c-7d8e-9f0a1b2c3d4e"), 
                new(21, "3c1d8e7f-1a2b-4c3d-8e9f-0a1b2c3d4e5f"), 
                new(22, "4d2e9f8a-2b3c-5d4e-9f0a-1b2c3d4e5f6a"), 
                new(23, "5e3f0a9b-3c4d-6e5f-0a1b-2c3d4e5f6a7b"), 
                new(24, "6f4a1b0c-4d5e-7f6a-1b2c-3d4e5f6a7b8c"), 
                new(25, "7a5b2c1d-5e6f-8a7b-2c3d-4e5f6a7b8c9d"),
                new(26, "8b6c3d2e-6f7a-9b8c-3d4e-5f6a7b8c9d0e"), 
                new(27, "9c7d4e3f-7a8b-0c9d-4e5f-6a7b8c9d0e1f"), 
                new(28, "0d8e5f4a-8b9c-1d0e-5f6a-7b8c9d0e1f2a"), 
                new(29, "1e9f6a5b-9c0d-2e1f-6a7b-8c9d0e1f2a3b"),
                new(30, "2f0a7b6c-0d1e-3f2a-7b8c-9d0e1f2a3b4c"), 
                new(31, "3a1b8c7d-1e2f-4a3b-8c9d-0e1f2a3b4c5e"), 
                new(32, "4b2c9d8f-2f3a-5b4c-9d0e-1f2a3b4c5d6f"), 
                new(33, "5c3d0e9a-3a4b-6c5d-0e1f-2a3b4c5d6e7a"),
                new(34, "6d4e1f0b-4b5c-7d6e-1f2a-3b4c5d6e7f8b"), 
                new(35, "7e5f2a1c-5c6d-8e7f-2a3b-4c5d6e7f8a9c")
            ];

        Result<PaginateResponse<KeyValuePair<int, string>>> resultPaginate =
            records.AsQueryable()
                .OrderBy(item => item.Key)
                .ToPaginate(
                    paginateHelperService.Validate(request)
                );

        ITaskPaginateResponse[] data = [];

        if (resultPaginate && resultPaginate.Data != null)
        {
            Result<ITaskPaginateRequest> resultRequest =
                await binderService.BindPaginateAsync(
                    new TaskCollectorRequest(TaskTypeConfiguration.Comment),
                    resultPaginate.Data.Data.Select(item => item.Value).Select(Guid.Parse),
                    cancellation
                );

            if (resultRequest && resultRequest.Data is not null)
            {
                Result<IEnumerable<ITaskPaginateResponse>> resultData =
                    await mediator.Send(resultRequest.Data, cancellation);

                if (resultData && resultData.Data is not null)
                    data = data.Concat(resultData.Data).ToArray();
            }

            result =
                Result.Success(
                    new PaginateResponse<ITaskPaginateResponse>(
                        data,
                        resultPaginate.Data.TotalRecords,
                        resultPaginate.Data.TotalPages,
                        resultPaginate.Data.Page,
                        resultPaginate.Data.Size
                    )
                );
        }

        return result;
    }
}