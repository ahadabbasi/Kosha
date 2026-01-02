using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;

public interface IUserService
{
    /// <summary>
    /// Paginate all users
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<PaginateResponse<AuthenticationResponse>>> PaginateAsync(
        PaginateRequest? request = null,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Find user by username
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<AuthenticationResponse>> FindByUsernameAsync(
        AuthenticationRequest request, 
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// get roles of user
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<IEnumerable<string>>> RolesAsync(
        AuthenticationRequest request, 
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Save the new user and hashed password
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> SaveAsync(
        AuthenticationSaveRequest request,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Update user information
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> UpdateAsync(
        AuthenticationUpdateRequest request,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Change password of user
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> ChangePasswordAsync(
        AuthenticationChangePasswordRequest request,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Checking the user hashed password is correct to plain text password
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result> VerifyPasswordAsync(
        AuthenticationVerifiedPasswordRequest request,
        CancellationToken cancellation = default
    );
}