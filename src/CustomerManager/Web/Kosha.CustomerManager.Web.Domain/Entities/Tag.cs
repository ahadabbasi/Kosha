using System;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Domain.Helper;

namespace Kosha.CustomerManager.Web.Domain.Entities;

public class Tag : IAudit
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public DateTime Inserted { get; set; }

    public DateTime? Modified { get; set; }

    public byte[] RowVersion { get; set; }

    public virtual ICollection<TagTask> Tasks { get; set; }
}