using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Authenticate;
using Kosha.CustomerManager.Web.Persistence.Contexts;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Persistence.Repositories;

internal sealed class UserRepository(ApplicationContext context) : AuditRepository<User>(context), IUserRepository
{
    public Task<User?> GetByUsernameAsync(string username) => 
        Query()
            .FirstOrDefaultAsync(PredicateUsername(username));

    public Task<bool> IsExistUsernameAsync(string username) 
        => Query()
            .AnyAsync(PredicateUsername(username));

    public async Task<IEnumerable<string>> UserRolesAsync(string username) =>
        await Query()
            .Where(PredicateUsername(username))
            .SelectMany(item => item.Roles.Select(userRole => userRole.Role.Name))
            .ToArrayAsync();


    private Expression<Func<User, bool>> PredicateUsername(string username)
        => model => model.Username.Equals(username);
}