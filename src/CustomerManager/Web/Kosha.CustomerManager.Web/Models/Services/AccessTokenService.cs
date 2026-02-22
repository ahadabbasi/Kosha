using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
using Kosha.CustomerManager.Web.Shared.Helper.Time;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Kosha.CustomerManager.Web.Models.Services;

internal sealed class AccessTokenService(
    ITimeService timeService,
    IOptions<BearerInformation> options
)
{
    private BearerInformation Information => options.Value;

    public AccessTokenResponse Generate(AccessTokenRequest request)
    {
        int validationTerm = request.ValidationTerm ?? Information.ValidationTerm;

        DateTime expirationTime = timeService.Now.AddSeconds(validationTerm);

        JwtSecurityToken securityToken =
            new JwtSecurityToken(
                issuer: Information.Issuer,
                audience: Information.Audience,
                claims: request.Claims,
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
            new AccessTokenResponse(
                securityTokenHandler.WriteToken(securityToken),
                validationTerm,
                expirationTime
            );
    }

    public bool TokenHasBeenExpire(string token)
    {
        return true;
    }

}