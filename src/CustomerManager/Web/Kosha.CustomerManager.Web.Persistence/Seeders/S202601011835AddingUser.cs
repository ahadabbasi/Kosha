using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Authenticate;
using Kosha.CustomerManager.Web.Persistence.Attributes;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Helper.Hasher.Algorithms;
using Kosha.CustomerManager.Web.Shared.Models.Hasher.Default;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Persistence.Seeders;

[Seed(202601011835)]
internal sealed class S202601011835AddingUser(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    IHasherService hasherService
) : IDataSeeder
{
    internal const string AhadUsername = "ahad";

    private readonly IDictionary<string, string> _users = 
            new Dictionary<string, string>
            {
                {
                    AhadUsername,
                    "09120276307"
                }
            };

    public async Task InvokeAsync(CancellationToken cancellation = default)
    {
        try
        {
            Result<HasherResponse> resultOfHash =
                await hasherService.HashAsync(new HasherRequest("1234"), cancellation);
            if (
                resultOfHash && 
                resultOfHash.Data is not null
            )
            {
                foreach ((string username, string phoneNumber) in _users)
                {
                    if (
                        !await repository.IsUsernameExistAsync(username) && 
                        !await repository.Query()
                            .AnyAsync(
                                item => item.PhoneNumber == phoneNumber,
                                cancellation
                            )
                    )
                    {
                        repository.Add(
                            new User
                            {
                                Username = username,
                                Password = resultOfHash.Data.Hashed,
                                PhoneNumber = phoneNumber
                            }
                        );

                        await unitOfWork.SaveChangesAsync(cancellation);

                    }
                }
            }
        }
        catch
        {
            //
        }
    }
}