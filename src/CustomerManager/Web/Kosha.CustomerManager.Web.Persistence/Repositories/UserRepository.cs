using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Authenticate;
using Kosha.CustomerManager.Web.Persistence.Contexts;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Persistence.Repositories;

internal sealed class UserRepository(
    ApplicationContext context
) : AuditRepository<User>(context),
    IUserRepository
{
    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellation = default) => 
        Query()
            .FirstOrDefaultAsync(
                PredicateUsername(username),
                cancellation
            );

    public Task<bool> IsUsernameExistAsync(string username, CancellationToken cancellation = default) => 
        Query()
            .AnyAsync(
                PredicateUsername(username),
                cancellation
            );

    public Task<bool> IsPhoneNumberExistAsync(string phoneNumber, CancellationToken cancellation = default) =>
        Query()
            .AnyAsync(
                item => item.PhoneNumber.Equals(phoneNumber),
                cancellation
            );

    public async Task<IEnumerable<string>> UserRolesAsync(string username, CancellationToken cancellation = default) =>
        await Query()
            .Where(PredicateUsername(username))
            .SelectMany(item => item.Roles.Select(userRole => userRole.Role.Name))
            .ToArrayAsync(cancellation);


    private Expression<Func<User, bool>> PredicateUsername(string username)
        => model => model.Username.Equals(username);
}