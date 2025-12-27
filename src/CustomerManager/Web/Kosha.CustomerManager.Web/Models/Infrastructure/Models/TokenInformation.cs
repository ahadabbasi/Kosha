namespace Kosha.CustomerManager.Web.Models.Infrastructure.Models;

public sealed class TokenInformation
{
    public int ValidationTerm { get; set; }

    public string Key { get; set; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;
}