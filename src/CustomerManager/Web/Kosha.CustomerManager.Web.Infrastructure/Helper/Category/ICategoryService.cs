using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Models.Category;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Category;

public interface ICategoryService
{
    /// <summary>
    /// All the categories
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<IEnumerable<CategoryResponse>> ListAsync(CancellationToken cancellation = default);

    /// <summary>
    /// Paginate all categories
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<PaginateResponse<CategoryResponse>>> PaginateAsync(
        PaginateRequest? request, CancellationToken cancellation = default
    );

    /// <summary>
    /// Create new category
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> CreateAsync(
        CategoryRequest entry, CancellationToken cancellation = default
    );

    /// <summary>
    /// Find category by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<CategoryResponse>> FindByIdAsync(Guid id, CancellationToken cancellation = default);

    /// <summary>
    /// Update the category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> UpdateAsync(Guid id, CategoryRequest request, CancellationToken cancellation = default);

    /// <summary>
    /// Delete the category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellation = default);

    /// <summary>
    /// Change the default category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> MakeDefaultAsync(Guid id, CancellationToken cancellation = default);

    /// <summary>
    /// Get the default category
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<Guid>> DefaultAsync(CancellationToken cancellation = default);
}