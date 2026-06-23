using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Helper;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Infrastructure.Services.Visitors;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Infrastructure.Extensions;

public static class ToPaginateExtension
{
    public static async Task<Result<PaginateResponse<TData>>> ToPaginateAsync<TData>(
        this IQueryable<TData> query,
        PaginateRequest request,
        CancellationToken cancellation = default
    )
    {
        Result<PaginateResponse<TData>> result = 
            Result.Failed<PaginateResponse<TData>>(Error.None);

        OrderingExpressionVisitor orderingVisitor =
            new OrderingExpressionVisitor();

        orderingVisitor.Visit(query.Expression);

        if (orderingVisitor.HasOrdering)
        {
            int totalRecords = //query.Count();
                await query.CountAsync(cancellation);

            int totalPages = (int)Math.Ceiling((decimal)totalRecords / request.Size);

            IList<TData> data =
                 await query
                    .Skip((request.Page - 1) * request.Size)
                    .Take(request.Size)
                    //.ToList();
                    .ToListAsync(cancellation);

            result =
                Result.Success(
                    new PaginateResponse<TData>(
                        data,
                        totalRecords,
                        totalPages,
                        request.Page,
                        data.Count
                    )
                );
        }

        return result; // Task.FromResult(result);
    }


    public static Result<PaginateResponse<TData>> ToPaginate<TData>(
        this IQueryable<TData> query, PaginateRequest request
    )
    {
        Result<PaginateResponse<TData>> result =
            Result.Failed<PaginateResponse<TData>>(Error.None);

        OrderingExpressionVisitor orderingVisitor =
            new OrderingExpressionVisitor();

        orderingVisitor.Visit(query.Expression);

        if (orderingVisitor.HasOrdering)
        {
            int totalRecords = query.Count();

            int totalPages = (int)Math.Ceiling((decimal)totalRecords / request.Size);

            IList<TData> data =
                query.Skip((request.Page - 1) * request.Size)
                    .Take(request.Size).ToList();

            result =
                Result.Success(
                    new PaginateResponse<TData>(
                        data,
                        totalRecords,
                        totalPages,
                        request.Page,
                        data.Count
                    )
                );
        }

        return result; // Task.FromResult(result);
    }

    public static Task<Result<PaginateResponse<TData>>> ToPaginateInsertedAsync<TData>(
        this IQueryable<TData> query,
        PaginateRequest request,
        CancellationToken cancellation = default
    )
        where TData : class, IInserted
        =>
            ToPaginateAsync(
                query.OrderBy(item => item.Inserted), 
                request, 
                cancellation
            );
}