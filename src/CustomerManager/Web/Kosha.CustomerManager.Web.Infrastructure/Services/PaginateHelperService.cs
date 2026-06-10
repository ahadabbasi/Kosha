using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;

namespace Kosha.CustomerManager.Web.Infrastructure.Services;

internal sealed class PaginateHelperService
{
    internal PaginateRequest Validate(PaginateRequest? request)
    {
        PaginateRequest defaultRequest = CreateDefault();

        if (request is null) 
            request = defaultRequest;

        if (request.Page <= 0)
            request = new PaginateRequest(defaultRequest.Page, request.Size);

        if (request.Size <= 0) 
            request = new PaginateRequest(request.Page, defaultRequest.Size);

        return request;
    }

    internal PaginateRequest CreateDefault() => 
        new(1, 10);
}