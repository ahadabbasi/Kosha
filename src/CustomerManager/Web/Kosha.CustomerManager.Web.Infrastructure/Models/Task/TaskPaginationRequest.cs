using System;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task;

public sealed record TaskPaginationRequest(Guid? Category, int Page, int Size) : 
    PaginateRequest(Page, Size);