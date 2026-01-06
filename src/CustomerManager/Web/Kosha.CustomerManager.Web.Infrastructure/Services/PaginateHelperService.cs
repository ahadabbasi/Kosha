using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;

namespace Kosha.CustomerManager.Web.Infrastructure.Services;

internal sealed class PaginateHelperService
{
    internal async Task<PaginateRequest> ValidateAsync(PaginateRequest? request)
    {
        PaginateRequest defaultRequest = await CreateDefaultAsync();

        if (request is null) 
            request = defaultRequest;

        if (request.Page <= 0)
            request = new PaginateRequest(defaultRequest.Page, request.Size);

        if (request.Size <= 0) 
            request = new PaginateRequest(request.Page, defaultRequest.Size);

        return request;
    }

    internal Task<PaginateRequest> CreateDefaultAsync() =>
        System.Threading.Tasks.Task.FromResult(
            new PaginateRequest(1, 10)
        );
}