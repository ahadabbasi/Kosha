using System;
using Kosha.CustomerManager.Web.Domain.Helper;

namespace Kosha.CustomerManager.Web.Domain.Authenticate;

public class RoleUser : IInserted, IRowVersion
{
    public Guid RoleId { get; set; }

    public virtual Role Role { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; }

    public DateTime Inserted { get; set; }

    public byte[] RowVersion { get; set; }
}