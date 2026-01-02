using System.Security.Claims;

namespace Kosha.CustomerManager.Web.Infrastructure.Configurations;

public class ClaimDefinitionConfiguration
{
    public const string Identifier = ClaimTypes.NameIdentifier;

    public const string Username = ClaimTypes.Name;

    public const string Name = ClaimTypes.GivenName;

    public const string Family = ClaimTypes.Surname;

    public const string Role = ClaimTypes.Role;

    public const string PhoneNumber = ClaimTypes.MobilePhone;
}