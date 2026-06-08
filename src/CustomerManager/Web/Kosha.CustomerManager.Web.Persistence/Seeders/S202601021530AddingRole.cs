using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Domain.Authenticate;
using Kosha.CustomerManager.Web.Persistence.Attributes;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Kosha.CustomerManager.Web.Shared.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Kosha.CustomerManager.Web.Persistence.Seeders;

[Seed(202601021530)]
internal sealed class S202601021530AddingRole(
    IAuditRepository<Role> repository, 
    IUnitOfWork unitOfWork
) : IDataSeeder
{
    public async Task InvokeAsync(CancellationToken cancellation = default)
    {
        try
        {
            foreach (string role in new[] { RoleNameConfiguration.User, RoleNameConfiguration.Admin})
                if (!await repository.Query().AnyAsync(item => item.Name.Equals(role), cancellation))
                {
                    repository.Add(new Role { Name = role });

                    await unitOfWork.SaveChangesAsync(cancellation);
                }
        }
        catch 
        {
            //
        }
        
    }
}