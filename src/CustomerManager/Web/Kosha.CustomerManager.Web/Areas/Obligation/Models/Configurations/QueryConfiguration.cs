using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Models.Configurations;

public class QueryConfiguration
{
    public const string Category = nameof(Category);

    public const string Area = nameof(Area);

    public const string Size = nameof(PaginateRequest.Size);

    public const string Page = nameof(PaginateRequest.Page);
}