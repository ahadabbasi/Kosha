using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Extensions;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Category;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Results;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Task;

internal sealed class TaskManagerService(
    IMediator mediator, ITaskBinderService binderService,
    ICategoryService categoryService, PaginateHelperService paginateHelperService,
    IEntityRepository<Domain.Entities.Task> repository, IUnitOfWork unitOfWork
) : ITaskManagerService
{
    public async System.Threading.Tasks.Task<Result<Guid>> CreateAsync(CancellationToken cancellation)
    {
        Result<Guid> result =
            Result.Failed<Guid>(ErrorConfiguration.TaskTypeInvalid);

        try
        {
            Result<ITaskCreateRequest> resultRequest = 
                await binderService.BindCreateAsync(cancellation);

            if (resultRequest && resultRequest.Data is not null)
            {
                result = await mediator.Send(resultRequest.Data, cancellation);

                if (result && result.Data != Guid.Empty)
                {
                    Result<Guid> defaultCategory = 
                        await categoryService.DefaultAsync(cancellation);

                    if (defaultCategory && defaultCategory.Data != Guid.Empty)
                    {
                        repository.Add(
                            new Domain.Entities.Task
                            {
                                Id = result,
                                CategoryId = defaultCategory,
                                Type = resultRequest.Data.Type
                            }
                        );

                        try
                        {
                            await unitOfWork.SaveChangesAsync(cancellation);
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }

        }
        catch (Exception)
        {
            //
        }

        return result;
    }

    public async System.Threading.Tasks.Task<Result<PaginateResponse<ITaskPaginateResponse>>> PaginateAsync(TaskPaginationRequest? request = null, CancellationToken cancellation = default)
    {
        Result<PaginateResponse<ITaskPaginateResponse>> result =
            Result.Failed<PaginateResponse<ITaskPaginateResponse>>(
                ErrorConfiguration.TaskTypeInvalid
            );

        Result<PaginateResponse<TaskRepositoryResponse>> resultPaginate =
            repository.Query()
                .OrderBy(item => item.Inserted)
                .Select(item => new TaskRepositoryResponse(item.Type, item.Id))
                .ToPaginate(
                    paginateHelperService.Validate(request)
                );

        if (resultPaginate && resultPaginate.Data != null)
        {
            ITaskPaginateResponse[] data = [];

            foreach (IGrouping<string, TaskRepositoryResponse> grouping in resultPaginate.Data.GroupBy(item => item.Type))
            {
                Result<ITaskPaginateRequest> resultRequest =
                    await binderService.BindPaginateAsync(
                        grouping.Key,
                        grouping.Select(item => item.Id),
                        cancellation
                    );

                if (resultRequest && resultRequest.Data is not null)
                {
                    Result<IEnumerable<ITaskPaginateResponse>> resultData =
                        await mediator.Send(resultRequest.Data, cancellation);

                    if (resultData && resultData.Data is not null)
                        data = data.Concat(resultData.Data).ToArray();
                }
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

    public async System.Threading.Tasks.Task<Result> ChangeCategoryAsync(Guid task, Guid category, CancellationToken cancellation)
    {
        Result result = ErrorConfiguration.TaskNotFound;

        Domain.Entities.Task? entity =
            await repository.GetByIdAsync(task, cancellation);

        if (entity != null && entity.CategoryId != category)
        {
            result = ErrorConfiguration.CategoryNotFound;

            entity.CategoryId = category;

            repository.Update(entity);

            try
            {
                await unitOfWork.SaveChangesAsync(cancellation);

                result = true;
            }
            catch (Exception )
            {
               // 
            }
        }

        return result;
    }


    public async System.Threading.Tasks.Task<Result<Guid>> CategoryAsync(Guid task, CancellationToken cancellation)
    {
        Result<Guid> result = Result.Failed<Guid>(ErrorConfiguration.TaskNotFound);

        Guid category =
            await repository.Query().Where(item => item.Id == task)
                .Select(item => item.CategoryId)
                .FirstOrDefaultAsync(cancellation);

        if (category != Guid.Empty)
            result = Result.Success(category);

        return result;
    }

    public async System.Threading.Tasks.Task<Result<ITaskDetailsResponse>> DetailsAsync(Guid task, CancellationToken cancellation = default)
    {
        string? type =
            await repository.Query().Where(item => item.Id.Equals(task))
                .Select(item => item.Type)
                .FirstOrDefaultAsync(cancellation);

        Result<ITaskDetailsResponse> result =
            Result.Failed<ITaskDetailsResponse>(ErrorConfiguration.TaskNotFound);

        if (!string.IsNullOrEmpty(type))
        {
            Result<ITaskDetailsRequest> resultRequest = 
                await binderService.BindDetailsAsync(type, task, cancellation);

            if (resultRequest && resultRequest.Data != null)
                result = await mediator.Send(resultRequest.Data, cancellation);
        }

        return result;
    }
}