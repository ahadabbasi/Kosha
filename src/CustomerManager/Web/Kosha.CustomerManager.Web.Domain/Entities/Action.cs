using System;
using Kosha.CustomerManager.Web.Domain.Authenticate;
using Kosha.CustomerManager.Web.Domain.Helper;

namespace Kosha.CustomerManager.Web.Domain.Entities;

public class Action : IAudit
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; }

    public Guid TaskId { get; set; }

    public virtual Task Task { get; set; }

    public DateTime Inserted { get; set; }
    
    public DateTime? Modified { get; set; }
    
    public byte[] RowVersion { get; set; }
    
}