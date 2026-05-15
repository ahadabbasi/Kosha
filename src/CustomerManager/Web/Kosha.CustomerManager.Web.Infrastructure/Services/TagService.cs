using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Entities;
using Kosha.CustomerManager.Web.Infrastructure.Extensions;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Tag;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Infrastructure.Models.Tag;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Infrastructure.Services;

internal sealed class TagService(
    PaginateHelperService paginateHelperService,
    ITagRepository repository,
    IRepository<TagTask> tagTaskRepository,
    IUnitOfWork unitOfWork
) : ITagService
{
    public async Task<Result<PaginateResponse<TagResponse>>> PaginateAsync(
        PaginateRequest? request = null,
        CancellationToken cancellation = default
    ) =>
        await repository.Query()
            .OrderBy(item => item.Inserted)
            .Select(Map())
            .ToPaginateAsync(
                await paginateHelperService.ValidateAsync(request),
                cancellation
            );

    public async Task<Result> CreateAsync(
        TagRequest request, 
        CancellationToken cancellation = default
    )
    {
        Result result = false;

        try
        {
            result = await repository.IsTitleExistAsync(request.Title, cancellation);

            if (!result)
            {
                Tag entity =
                    new Tag
                    {
                        Title = request.Title
                    };

                repository.Add(entity);

                try
                {
                    await unitOfWork.SaveChangesAsync(cancellation);

                    result = true;
                }
                catch
                {
                    //
                }
            }
        }
        catch
        {
            //
        }

        return result;
    }

    public async Task<Result<TagResponse>> FindByIdAsync(
        Guid id, 
        CancellationToken cancellation = default
    )
    {
        Result<TagResponse> result =
            Result.Failed<TagResponse>(Error.None);

        IQueryable<Tag> query = 
                repository.Query()
                    .Where(PredicateForId(id));

        if (await query.AnyAsync(cancellation))
            result =
                Result.Success(
                    await query.Select(Map()).FirstAsync(cancellation)
                );
        
        return result;
    }

    public async Task<Result> UpdateAsync(
        Guid id, 
        TagRequest request, 
        CancellationToken cancellation = default
    )
    {
        Result result = false;

        IQueryable<Tag> query =
            repository.Query()
                .Where(PredicateForId(id));

        if (await query.AnyAsync(cancellation))
        {
            if (
                ! await repository.Query()
                    .AnyAsync(
                        item => item.Id != id && item.Title.Equals(request.Title),
                        cancellation
                    )
            )
            {
                Tag entity =
                    await query.FirstAsync(cancellation);

                entity.Title = request.Title;

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
        }

        return result;
    }

    public async Task<Result> DeleteAsync(
        Guid id, 
        CancellationToken cancellation = default
    )
    {
        Result result = false;

        IQueryable<Tag> query =
            repository.Query()
                .Where(PredicateForId(id));

        if (await query.AnyAsync(cancellation))
        {
            Tag entity =
                await query.FirstAsync(cancellation);

            repository.Delete(entity);

            try
            {
                await unitOfWork.SaveChangesAsync(cancellation);

                result = true;
            }
            catch
            {
                //
            }
        }

        return result;
    }

    public async Task<Result<IEnumerable<TagResponse>>> FetchTaskTagsAsync(
        Guid task, 
        CancellationToken cancellation = default
    ) => 
        Result.Success<IEnumerable<TagResponse>>(
            await tagTaskRepository.Query()
                .Where(item => item.TaskId.Equals(task))
                .Select(item => item.Tag)
                .Select(Map())
                .ToArrayAsync(cancellation)
        );

    public async Task<Result<IEnumerable<TagResponse>>> SearchTagsAsync(
        string? title, 
        CancellationToken cancellation = default
    )
    {
        IQueryable<Tag> query = repository.Query();

        if (!string.IsNullOrEmpty(title))
            query = query.Where(item => item.Title.Contains(title));

        return Result.Success<IEnumerable<TagResponse>>(
            await query
                .OrderBy(item => item.Inserted)
                .Take(10)
                .Select(Map())
                .ToArrayAsync(cancellation)
        );
    }

    public async Task<Result> AttachTagToTaskAsync(
        Guid task, 
        Guid tag, 
        CancellationToken cancellation = default
    )
    {
        Result result = false;

        if (
            tag != Guid.Empty &&
            ! await tagTaskRepository.Query()
                .AnyAsync(
                    item => item.TaskId.Equals(task) && item.Tag.Equals(tag),
                    cancellation
                )
        )
        {
            TagTask entity =
                new TagTask
                {
                    TaskId = task,
                    TagId = tag
                };

            tagTaskRepository.Add(entity);

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

    public async Task<Result> DetachTagFromTaskAsync(
        Guid task, 
        Guid tag, 
        CancellationToken cancellation = default
    )
    {
        Result result = false;

        if (tag != Guid.Empty)
        {
            IQueryable<TagTask> query =
                tagTaskRepository.Query()
                    .Where(item => item.TaskId.Equals(task) && item.Tag.Equals(tag));

            if (await query.AnyAsync(cancellation))
            {
                TagTask entity = await query.FirstAsync(cancellation);

                tagTaskRepository.Delete(entity);

                try
                {
                    await unitOfWork.SaveChangesAsync(cancellation);

                    result = true;
                }
                catch
                {
                    //
                }
            }
        }

        return result;
    }

    private Expression<Func<Tag, bool>> PredicateForId(Guid id) =>
        item => item.Id.Equals(id);

    private Expression<Func<Tag, TagResponse>> Map() => 
        item => new TagResponse(item.Id, item.Title);
}