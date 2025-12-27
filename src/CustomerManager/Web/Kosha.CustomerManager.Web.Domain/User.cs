using Kosha.CustomerManager.Web.Domain.Helper;

namespace Kosha.CustomerManager.Web.Domain;

public sealed class User : IAudit
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public DateTime Inserted { get; set; }
    public DateTime? Modified { get; set; }
}