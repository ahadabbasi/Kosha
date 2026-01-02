using System.Security.Claims;

namespace Kosha.CustomerManager.Web.Infrastructure.Configurations;

public class ClaimDefinitionConfiguration
{
    public const string Identifier = ClaimTypes.NameIdentifier;

    public const string Name = ClaimTypes.Name;

    public const string Family = ClaimTypes.Surname;

    public const string Role = ClaimTypes.Role;

    public const string PhoneNumber = ClaimTypes.MobilePhone;
}