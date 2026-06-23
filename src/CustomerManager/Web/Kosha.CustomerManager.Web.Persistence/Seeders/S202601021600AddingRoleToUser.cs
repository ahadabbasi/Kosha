using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Authenticate;
using Kosha.CustomerManager.Web.Persistence.Attributes;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Persistence.Seeders;

[Seed(202601021600)]
internal sealed class S202601021600AddingRoleToUser(
    IUserRepository userRepository,
    IEntityRepository<Role> roleRepository,
    IRepository<RoleUser> roleUserRepository,
    IUnitOfWork unitOfWork
) : IDataSeeder
{
    public async Task InvokeAsync(CancellationToken cancellation = default)
    {
        IQueryable<User> userQuery =
            userRepository.Query()
                .Where(item => item.Username.Equals(S202601011835AddingUser.AhadUsername));

        IQueryable<Role> roleQuery =
            roleRepository.Query()
                .Where(item => item.Name.Equals(RoleNameConfiguration.Admin));
        try
        {
            if (await userQuery.AnyAsync(cancellation) && await roleQuery.AnyAsync(cancellation))
            {
                Guid user =
                    await userQuery.Select(item => item.Id)
                        .FirstAsync(cancellation);

                Guid role =
                    await roleQuery.Select(item => item.Id)
                        .FirstAsync(cancellation);

                if (
                    ! await roleUserRepository.Query()
                        .AnyAsync(
                            item => item.UserId.Equals(user) && item.RoleId.Equals(role),
                            cancellation
                        )
                )
                {
                    roleUserRepository.Add(
                        new RoleUser
                        {
                            UserId = user,
                            RoleId = role
                        }
                    );

                    await unitOfWork.SaveChangesAsync(cancellation);
                }
            }
        }
        catch
        {
            //
        }

    }
}