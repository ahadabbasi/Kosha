using System;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Domain.Helper;

namespace Kosha.CustomerManager.Web.Domain.Entities;

public class Customer : IAudit
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Family { get; set; }

    public DateTime Inserted { get; set; }

    public DateTime? Modified { get; set; }

    public byte[] RowVersion { get; set; }

    public virtual ICollection<CustomerContact> Contacts { get; set; }

    public virtual ICollection<Task> Tasks { get; set; }
}