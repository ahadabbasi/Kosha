using System;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public sealed record CreatePaginateNavbarVm(
    PaginateResponse Paginate, 
    Func<int, string?> LinkGenerator
);