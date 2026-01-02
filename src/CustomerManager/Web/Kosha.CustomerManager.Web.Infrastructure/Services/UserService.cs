using System;
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
using Kosha.CustomerManager.Web.Shared.Helper.Hasher.Algorithms;
using Kosha.CustomerManager.Web.Shared.Models.Hasher.Default;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Infrastructure.Services;

internal sealed class UserService(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    IHasherService hasherService
) : IUserService
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

        User? entity = await repository.GetByUsernameAsync(request.Username, cancellationToken);

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

        if (await repository.IsExistUsernameAsync(request.Username, cancellationToken))
        {
            result =
                Result.Success(
                    await repository.UserRolesAsync(request.Username, cancellationToken)
                );
        }

        return result;
    }

    public async Task<Result> SaveAsync(
        AuthenticationSaveRequest request,
        CancellationToken cancellation = default
    )
    {
        Result result =
            Result.Failed(
                ErrorConfiguration.UsernameExist
            );

        try
        {
            bool exist =
                await repository.IsExistUsernameAsync(request.Username, cancellation);

            if (!exist)
            {
                result =
                    Result.Failed(
                        ErrorConfiguration.PhoneNumberExist
                    );

                exist =
                    await repository.Query()
                        .Where(item => item.PhoneNumber.Equals(request.PhoneNumber))
                        .AnyAsync(cancellation);

                if (!exist)
                {
                    result = false;

                    Result<HasherResponse> resultOfHasher =
                        await hasherService.HashAsync(
                            new HasherRequest(request.Password),
                            cancellation
                        );

                    if (
                        resultOfHasher &&
                        resultOfHasher.Data != null
                    )
                    {
                        repository.Add(
                            new User
                            {
                                Username = request.Username,
                                Password = resultOfHasher.Data.Hashed,
                                PhoneNumber = request.PhoneNumber,
                                Name = request.Name,
                                Family = request.Family
                            }
                        );

                        await unitOfWork.SaveChangesAsync(cancellation);

                        result = true;
                    }

                }
            }
        }
        catch (Exception)
        {
            //
        }

        return result;
    }

    public async Task<Result> UpdateAsync(
        Guid id,
        AuthenticationUpdateRequest request, 
        CancellationToken cancellation = default
    )
    {
        Result result =
            Result.Failed(
                ErrorConfiguration.UsernameExist
            );

        try
        {
            IQueryable<User> query =
                repository.Query()
                    .Where(item => item.Id != id);

            bool exist =
                await query
                    .AnyAsync(
                        item => item.Username.Equals(request.Username),
                        cancellation
                    );

            if (!exist)
            {
                result =
                    Result.Failed(
                        ErrorConfiguration.PhoneNumberExist
                    );

                exist =
                    await query
                        .AnyAsync(
                            item => item.PhoneNumber.Equals(request.PhoneNumber),
                            cancellation
                        );

                if (!exist)
                {
                    result = false;

                    User? entity =
                        await repository.GetByIdAsync(id);

                    if (entity != null)
                    {
                        entity.Name = request.Name; 
                        entity.Family = request.Family;
                        entity.Username = request.Username;
                        entity.PhoneNumber = request.PhoneNumber;

                        repository.Update(entity);

                        await unitOfWork.SaveChangesAsync(cancellation);

                        result = true;
                    }

                }
            }
        }
        catch (Exception)
        {
            //
        }

        return result;
    }

    public async Task<Result> ChangePasswordAsync(
        AuthenticationChangePasswordRequest request,
        CancellationToken cancellation = default
    )
    {
        Result result =
            await VerifyPasswordAsync(request, cancellation);

        try
        {
            if (result)
            {
                result = false;

                Result<HasherResponse> resultOfHasher =
                    await hasherService.HashAsync(
                        new HasherRequest(request.NewPassword),
                        cancellation
                    );

                if (
                    resultOfHasher &&
                    resultOfHasher.Data != null
                )
                {
                    User? entity =
                        await repository.GetByUsernameAsync(request.User.Username, cancellation);

                    if (entity != null)
                    {
                        entity.Password = resultOfHasher.Data.Hashed;

                        repository.Update(entity);

                        await unitOfWork.SaveChangesAsync(cancellation);

                        result = true;
                    }
                }
            }
        }
        catch
        {
            //
        }

        return result;
    }

    public Task<Result> VerifyPasswordAsync(
        AuthenticationVerifiedPasswordRequest request,
        CancellationToken cancellation = default
    ) =>
        hasherService.VerifyAsync(
            request,
            new HasherResponse(request.User.Password),
            cancellation
        );
}