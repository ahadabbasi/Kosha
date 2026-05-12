using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Infrastructure.Models.Tag;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Tag;

public interface ITagService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<PaginateResponse<TagResponse>>> PaginateAsync(
        PaginateRequest? request = null,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Create tag
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> CreateAsync(TagRequest request, CancellationToken cancellation = default);

    /// <summary>
    /// Find tag by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<TagResponse>> FindByIdAsync(Guid id, CancellationToken cancellation = default);

    /// <summary>
    /// Update the tag
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> UpdateAsync(Guid id, TagRequest request, CancellationToken cancellation = default);

    /// <summary>
    /// Delete the tag
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellation = default);

    /// <summary>
    /// Get all tags has been added to task
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<IEnumerable<TagResponse>>> FetchTaskTagsAsync(Guid task, CancellationToken cancellation = default);

    /// <summary>
    /// Search tag with title tag
    /// </summary>
    /// <param name="title"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<IEnumerable<TagResponse>>> SearchTagsAsync(string? title, CancellationToken cancellation = default);

    /// <summary>
    /// Attach a tag to the task
    /// </summary>
    /// <param name="task"></param>
    /// <param name="tag"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> AttachTagToTaskAsync(Guid task, Guid tag, CancellationToken cancellation = default);

    /// <summary>
    /// Detach tag from task
    /// </summary>
    /// <param name="task"></param>
    /// <param name="tag"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> DetachTagFromTaskAsync(Guid task, Guid tag, CancellationToken cancellation = default);
}