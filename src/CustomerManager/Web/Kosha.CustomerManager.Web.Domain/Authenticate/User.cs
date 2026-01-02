using System;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Domain.Helper;

namespace Kosha.CustomerManager.Web.Domain.Authenticate;

public class User : IAudit
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public string Family { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime Inserted { get; set; }

    public DateTime? Modified { get; set; }

    public byte[] RowVersion { get; set; }

    public virtual ICollection<RoleUser> Roles { get; set; }

    public virtual ICollection<Domain.Entities.Action> Actions { get; set; }
}