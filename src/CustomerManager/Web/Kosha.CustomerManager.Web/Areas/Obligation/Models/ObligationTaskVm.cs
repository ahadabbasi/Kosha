using System;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Models;

public sealed record ObligationTaskVm(
    IEnumerable<ITaskPaginateResponse> Data,
    int TotalRecords,
    int TotalPages,
    int Page,
    int Size,
    Guid Task
) : PaginateResponse<ITaskPaginateResponse>(Data, TotalRecords, TotalPages, Page, Size)
{
    public ObligationTaskVm(Guid Task, PaginateResponse<ITaskPaginateResponse> paginate) :
        this(
            paginate.Data, paginate.TotalRecords, 
            paginate.TotalPages, paginate.Page, 
            paginate.Size, Task
        )
    {
        
    }
}