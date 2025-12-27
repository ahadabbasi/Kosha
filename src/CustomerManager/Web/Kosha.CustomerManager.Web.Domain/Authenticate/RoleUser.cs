using System;

namespace Kosha.CustomerManager.Web.Domain.Authenticate;

public class RoleUser
{
    public Guid RoleId { get; set; }

    public virtual Role Role { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; }

    public DateTime Inserted { get; set; }

    public byte[] RowVersion { get; set; }
}