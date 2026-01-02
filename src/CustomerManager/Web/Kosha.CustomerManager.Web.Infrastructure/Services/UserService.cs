using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Authenticate;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Extensions;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Services;

internal sealed class UserService(IUserRepository repository) : IUserService
{
    public Task<Result<PaginateResponse<AuthenticationResponse>>> PaginateAsync(
        PaginateRequest? request = null, 
        CancellationToken cancellation = default
    )
    {
        request ??= new PaginateRequest(1, 10);

        return 
            repository.Query()
                .OrderBy(entity => entity.Inserted)
                .Select(entity =>
                    new AuthenticationResponse(
                        entity.Id,
                        entity.Username,
                        string.Empty,
                        entity.Name,
                        entity.Family,
                        entity.PhoneNumber
                    )
                )
                .ToPaginateAsync(
                    request, 
                    cancellation
                );
    }

    public async Task<Result<AuthenticationResponse>> FindByUsernameAsync(
        AuthenticationRequest request, 
        CancellationToken cancellationToken = default
    )
    {
        Result<AuthenticationResponse> result =
            Result.Failed<AuthenticationResponse>(
                ErrorConfiguration.UsernameNotFound
            );

        User? entity = await repository.GetByUsernameAsync(request.Username);

        if (entity is not null)
        {
            result =
                Result.Success(
                    new AuthenticationResponse(
                        entity.Id,
                        entity.Username,
                        entity.Password,
                        entity.Name,
                        entity.Family,
                        entity.PhoneNumber
                    )
                );
        }

        return result;
    }

    public async Task<Result<IEnumerable<string>>> RolesAsync(
        AuthenticationRequest request, 
        CancellationToken cancellationToken = default
    )
    {
        Result<IEnumerable<string>> result =
            Result.Failed<IEnumerable<string>>(
                ErrorConfiguration.UsernameNotFound
            );

        if (await repository.IsExistUsernameAsync(request.Username))
        {
            result =
                Result.Success(
                    await repository.UserRolesAsync(request.Username)
                );
        }

        return result;
    }
}