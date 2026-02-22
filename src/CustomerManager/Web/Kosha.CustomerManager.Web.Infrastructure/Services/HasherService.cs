using System;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Shared.Helper.Hasher.Algorithms;
using Kosha.CustomerManager.Web.Shared.Models.Hasher.Default;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Kosha.CustomerManager.Web.Infrastructure.Services;

internal sealed class HasherService : IHasherService
{
    public Task<Result<HasherResponse>> HashAsync(
        HasherRequest request, 
        CancellationToken cancellationToken = default
    ) =>
        System.Threading.Tasks.Task.FromResult(
            Result.Success(
                new HasherResponse(
                    Convert.ToBase64String(
                        KeyDerivation.Pbkdf2(
                            password: request.PlainText,
                            salt: new byte[128 / 8],
                            prf: KeyDerivationPrf.HMACSHA1,
                            iterationCount: 10000,
                            numBytesRequested: 256 / 8
                        )
                    )
                )
            )
        );

    public async Task<Result> VerifyAsync(
        HasherRequest request, 
        HasherResponse hashed, 
        CancellationToken cancellationToken = default
    )
    {
        Result result = ErrorConfiguration.HashedNotMatch;

        Result<HasherResponse> resultOfHash =
            await HashAsync(
                request, 
                cancellationToken
            );

        if (
            resultOfHash.IsSuccess &&
            resultOfHash.Data != null &&
            !string.IsNullOrEmpty(resultOfHash.Data.Hashed)
        )
            if (resultOfHash.Data.Hashed.Equals(hashed.Hashed)) 
                result = true;

        return result;
    }
}