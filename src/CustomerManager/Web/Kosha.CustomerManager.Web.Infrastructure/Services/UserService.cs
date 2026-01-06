using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Authenticate;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Extensions;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Helper.Hasher.Algorithms;
using Kosha.CustomerManager.Web.Shared.Models.Hasher.Default;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Infrastructure.Services;

internal sealed class UserService(
    PaginateHelperService paginateHelperService,
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    IHasherService hasherService
) : IUserService
{
    public async Task<Result<PaginateResponse<UserResponse>>> PaginateAsync(
        PaginateRequest? request = null,
        CancellationToken cancellation = default
    ) =>
        await repository.Query()
            .OrderBy(entity => entity.Inserted)
            .Select(entity =>
                new UserResponse(
                    entity.Id,
                    entity.Username,
                    string.Empty,
                    entity.Name,
                    entity.Family,
                    entity.PhoneNumber
                )
            )
            .ToPaginateAsync(
                await paginateHelperService.ValidateAsync(request),
                cancellation
            );

    public async Task<Result<UserResponse>> FindByUsernameAsync(
        UserRequest request,
        CancellationToken cancellationToken = default
    )
    {
        Result<UserResponse> result =
            Result.Failed<UserResponse>(
                ErrorConfiguration.UsernameNotFound
            );

        User? entity = await repository.GetByUsernameAsync(request.Username, cancellationToken);

        if (entity is not null)
        {
            result =
                Result.Success(
                    new UserResponse(
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
        UserRequest request,
        CancellationToken cancellationToken = default
    )
    {
        Result<IEnumerable<string>> result =
            Result.Failed<IEnumerable<string>>(
                ErrorConfiguration.UsernameNotFound
            );

        if (await repository.IsUsernameExistAsync(request.Username, cancellationToken))
        {
            result =
                Result.Success(
                    await repository.UserRolesAsync(request.Username, cancellationToken)
                );
        }

        return result;
    }

    public async Task<Result> SaveAsync(
        UserSaveRequest request,
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
                await repository.IsUsernameExistAsync(request.Username, cancellation);

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

                        try
                        {
                            await unitOfWork.SaveChangesAsync(cancellation);

                            result = true;
                        }
                        catch
                        {
                            //
                        }
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

    public async Task<Result> UpdateAsync(
        Guid id,
        UserUpdateRequest request,
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

                        try
                        {
                            await unitOfWork.SaveChangesAsync(cancellation);

                            result = true;
                        }
                        catch
                        {
                            //
                        }
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

    public async Task<Result> ChangePasswordAsync(
        UserChangePasswordRequest request,
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

                        try
                        {
                            await unitOfWork.SaveChangesAsync(cancellation);

                            result = true;
                        }
                        catch
                        {
                            //
                        }
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
        UserVerifiedPasswordRequest request,
        CancellationToken cancellation = default
    ) =>
        hasherService.VerifyAsync(
            request,
            new HasherResponse(request.User.Password),
            cancellation
        );
}