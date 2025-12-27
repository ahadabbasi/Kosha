using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Models.Infrastructure.Helper;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
using Kosha.CustomerManager.Web.Shared.Helper.Time;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Kosha.CustomerManager.Web.Models.Services;

internal sealed class TokenService(ITimeService timeService, IOptions<TokenInformation> options) : ITokenService
{
    private TokenInformation Information => options.Value;

    public Task<Result<TokenResponse>> GenerateAsync(
        AuthenticationResponse request,
        IEnumerable<string>? roles = null,
        CancellationToken cancellation = default
    )
    {
        IList<Claim> claims =
            new List<Claim>
            {
                new(ClaimDefinitionConfiguration.Identifier, request.Id.ToString()),
                new (ClaimDefinitionConfiguration.Name, request.Name),
                new (ClaimDefinitionConfiguration.Family, request.Family),
            };

        if (
            roles != null &&
            roles.Any()
        )
        {
            claims.Add(
                new Claim(ClaimDefinitionConfiguration.Role, string.Join(", ", roles))
            );
        }

        DateTime expirationTime = timeService.Now.AddSeconds(Information.ValidationTerm);

        JwtSecurityToken securityToken =
            new JwtSecurityToken(
                issuer: Information.Issuer,
                audience: Information.Audience,
                claims: claims,
                expires: expirationTime,
                signingCredentials: 
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Information.Key)
                    ),
                    SecurityAlgorithms.HmacSha256
                )
            );

        JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();

        return
            Task.FromResult(
                Result.Success(
                    new TokenResponse(
                        securityTokenHandler.WriteToken(securityToken),
                        Information.ValidationTerm,
                        expirationTime
                    )
                )
            );
    }
}