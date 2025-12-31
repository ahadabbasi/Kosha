using System;
using Kosha.CustomerManager.Web.Domain.Helper;

namespace Kosha.CustomerManager.Web.Domain.Entities;

public class CustomerContact : IAudit
{
    
    public Guid Id { get; set; }

    public string Type { get; set; }

    public string Value { get; set; }

    public Guid CustomerId { get; set; }

    public virtual Customer Customer { get; set; }

    public DateTime Inserted { get; set; }

    public DateTime? Modified { get; set; }

    public byte[] RowVersion { get; set; }
}