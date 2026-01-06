using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;
using Kosha.CustomerManager.Web.Shared.Models.Configurations;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Models.Services;

internal sealed class AuthenticationClaimGeneratorService(IUserService service)
{
    internal async Task<IEnumerable<Claim>> CreateAsync(UserResponse request, CancellationToken cancellation = default)
    {
        IList<Claim> result =
            new List<Claim>
            {
                new(
                    ClaimDefinitionConfiguration.Identifier,
                    request.Id.ToString()
                ),
                new(
                    ClaimDefinitionConfiguration.Username,
                    request.Username
                ),
                new(
                    ClaimDefinitionConfiguration.PhoneNumber,
                    request.PhoneNumber
                )
            };

        foreach ((string type, string? value) in
                 new Dictionary<string, string?>
                 {
                     {
                         ClaimDefinitionConfiguration.Name,
                         request.Name
                     },
                     {
                         ClaimDefinitionConfiguration.Family,
                         request.Family
                     }
                 }
                )
        {
            if (!string.IsNullOrEmpty(value))
            {
                result.Add(new Claim(type, value));
            }
        }

        Result<IEnumerable<string>> resultOfRoles =
            await service.RolesAsync(
                request, 
                cancellation
            );

        IList<string> roles = new List<string>();

        if (
            resultOfRoles &&
            resultOfRoles.Data != null &&
            resultOfRoles.Data.Any()
        )
            foreach (string role in resultOfRoles.Data)
                roles.Add(role);

        if (!roles.Any(item => item.Equals(RoleNameConfiguration.User, StringComparison.OrdinalIgnoreCase)))
            roles.Add(RoleNameConfiguration.User);

        foreach (string role in roles)
        {
            result.Add(
                new Claim(
                    ClaimDefinitionConfiguration.Role,
                    role
                )
            );
        }

        return result;
    }
}