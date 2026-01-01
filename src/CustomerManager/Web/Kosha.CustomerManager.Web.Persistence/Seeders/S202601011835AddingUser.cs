using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Authenticate;
using Kosha.CustomerManager.Web.Persistence.Attributes;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Helper.Hasher.Algorithms;
using Kosha.CustomerManager.Web.Shared.Models.Hasher.Default;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Persistence.Seeders;

[Seed(202601011835)]
public sealed class S202601011835AddingUser(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    IHasherService hasherService
) : IDataSeeder
{
    private readonly IEnumerable<string> _users = ["ahad"];

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
                foreach (string user in _users)
                {
                    if (!await repository.IsExistUsernameAsync(user))
                    {
                        repository.Add(
                            new User()
                            {
                                Username = user,
                                Password = resultOfHash.Data.Hashed
                            }
                        );

                        await unitOfWork.SaveChangesAsync(cancellation);

                    }
                }
            }
        }
        catch(Exception exception)
        {
            //
        }
    }
}