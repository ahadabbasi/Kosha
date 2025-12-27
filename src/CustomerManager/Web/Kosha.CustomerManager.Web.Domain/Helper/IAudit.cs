namespace Kosha.CustomerManager.Web.Domain.Helper;

public interface IAudit
{
    Guid Id { get; set; }

    DateTime Inserted { get; set; }

    DateTime? Modified { get; set; }
}