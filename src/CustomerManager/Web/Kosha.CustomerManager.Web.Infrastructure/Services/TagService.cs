using System;
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

    public async Task<Result> CreateAsync(TagRequest request, CancellationToken cancellation = default)
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

    public async Task<Result<TagResponse>> FindByIdAsync(Guid id, CancellationToken cancellation = default)
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

    public async Task<Result> UpdateAsync(Guid id, TagRequest request, CancellationToken cancellation = default)
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

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellation = default)
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

    private Expression<Func<Tag, bool>> PredicateForId(Guid id) =>
        item => item.Id.Equals(id);

    private Expression<Func<Tag, TagResponse>> Map() => 
        item => new TagResponse(item.Id, item.Title);
}