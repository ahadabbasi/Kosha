using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Kosha.CustomerManager.Web.Domain.Entities;
using Kosha.CustomerManager.Web.Domain.Enums;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Extensions;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Category;
using Kosha.CustomerManager.Web.Infrastructure.Models.Category;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Helper;
using Kosha.CustomerManager.Web.Shared.Models;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Infrastructure.Services;

internal sealed class CategoryService(
    IAuditRepository<Category> repository, 
    IEnumCaptionService enumCaptionService,
    IUnitOfWork unitOfWork,
    PaginateHelperService paginateHelperService
) : ICategoryService
{
    private IEnumerable<EnumCaptionResponse<AnsEnum>> Ans() =>
        enumCaptionService.Convert<AnsEnum>();

    public async System.Threading.Tasks.Task<IEnumerable<CategoryResponse>> ListAsync(CancellationToken cancellation = default)
    {
        IEnumerable<EnumCaptionResponse<AnsEnum>> ans = Ans();

        return (
            await repository.Query()
                .OrderByDescending(item => item.Inserted)
                .Select(item =>
                    new CategoryRepositoryResponse(
                        item.Id, item.Name,
                        item.IsDefault, item.Inserted
                    )
                ).ToListAsync(cancellation)
        ).Select(response =>
            new CategoryResponse(
                response.Id,
                response.Name,
                ans.First(item => item.Value == response.IsDefault)
            )
        );
    }

    public async System.Threading.Tasks.Task<Result<PaginateResponse<CategoryResponse>>> PaginateAsync(PaginateRequest? request, CancellationToken cancellation = default)
    {
        Result<PaginateResponse<CategoryRepositoryResponse>> paginate =
            await repository.Query()
                .OrderByDescending(item => item.Inserted)
                .Select(item =>
                    new CategoryRepositoryResponse(
                        item.Id, item.Name, 
                        item.IsDefault, item.Inserted
                    )
                )
                .ToPaginateAsync(paginateHelperService.Validate(request), cancellation);

        
        PaginateResponse<CategoryResponse> data =
            new PaginateResponse<CategoryResponse>([], 0, 0, 0, 0);
        
        if (paginate && paginate.Data != null)
        {
            IList<CategoryResponse> records = new List<CategoryResponse>();

            IEnumerable<EnumCaptionResponse<AnsEnum>> ans = Ans();

            foreach (CategoryRepositoryResponse response in paginate.Data)
                records.Add(
                    new CategoryResponse(
                        response.Id,
                        response.Name,
                        ans.First(item => item.Value == response.IsDefault)
                    )
                );

            data = 
                new PaginateResponse<CategoryResponse>(
                    records, paginate.Data.TotalRecords,
                    paginate.Data.TotalPages, paginate.Data.Page, paginate.Data.Size
                );
        }

        return Result.Success(data);
    }

    public async System.Threading.Tasks.Task<Result> CreateAsync(
        CategoryRequest entry, CancellationToken cancellation = default
    )
    {
        Result result = ErrorConfiguration.CategoryAlreadyExist;

        if (!await repository.Query().AnyAsync(item => item.Name.Equals(entry.Name), cancellation))
        {
            Category entity =
                new Category
                {
                    Name = entry.Name, 
                    IsDefault = AnsEnum.No
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

        return result;
    }

    public async System.Threading.Tasks.Task<Result<CategoryResponse>> FindByIdAsync(
        Guid id, CancellationToken cancellation = default
    )
    {
        Result<CategoryResponse> result =
            Result.Failed<CategoryResponse>(ErrorConfiguration.CategoryNotFound);

        Category? entity = await repository.GetByIdAsync(id, cancellation);

        if (entity != null)
            result =
                Result.Success(
                    new CategoryResponse(
                        entity.Id,
                        entity.Name,
                        Ans().First(item => item.Value == entity.IsDefault)
                    )
                );

        return result;
    }

    public async System.Threading.Tasks.Task<Result> UpdateAsync(
        Guid id, CategoryRequest request, CancellationToken cancellation = default
    )
    {
        Result result = ErrorConfiguration.CategoryAlreadyExist;

        if (!await repository.Query()
                .AnyAsync(item => 
                    item.Id != id && item.Name.Equals(request.Name),
                    cancellation
                )
        )
        {
            result = ErrorConfiguration.CategoryNotFound;

            Category? entity = await repository.GetByIdAsync(id, cancellation);

            if (entity != null)
            {
                entity.Name = request.Name;

                repository.Update(entity);

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

    public async System.Threading.Tasks.Task<Result> DeleteAsync(
        Guid id, CancellationToken cancellation = default
    )
    {
        Result result = ErrorConfiguration.CategoryNotFound;

        Category? entity = await repository.GetByIdAsync(id, cancellation);

        if (entity != null)
        {
            repository.Delete(entity);

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

    public async System.Threading.Tasks.Task<Result> MakeDefaultAsync(Guid id, CancellationToken cancellation = default)
    {
        Result result = ErrorConfiguration.CategoryNotFound;

        Category? category = await repository.GetByIdAsync(id, cancellation);

        if (category != null)
        {
            result = true;

            if (category.IsDefault == AnsEnum.No)
            {
                result = Error.None;

                Category @default =
                    await repository.Query()
                        .Where(item => item.IsDefault == AnsEnum.Yes)
                        .FirstAsync(cancellation);

                //ITransaction transaction = unitOfWork.Transaction();

                category.IsDefault = AnsEnum.Yes;
                @default.IsDefault = AnsEnum.No;

                repository.Update(category);
                repository.Update(@default);

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


    public async System.Threading.Tasks.Task<Result<Guid>> DefaultAsync(CancellationToken cancellation = default)
    {
        Result<Guid> result = Result.Failed<Guid>(ErrorConfiguration.CategoryNotFound);

        Guid category = await repository.Query()
            .Where(item => item.IsDefault == AnsEnum.Yes)
            .Select(item => item.Id)
            .FirstOrDefaultAsync(cancellation);

        if (category != Guid.Empty) 
            result = Result.Success(category);

        return result;
    }
}