using Kosha.CustomerManager.Web.Domain.Helper;
using System;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Domain.Enums;

namespace Kosha.CustomerManager.Web.Domain.Entities;

public class Category : IAudit
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public AnsEnum IsDefault { get; set; }

    public DateTime Inserted { get; set; }
    
    public DateTime? Modified { get; set; }
    
    public byte[] RowVersion { get; set; }

    public ICollection<Task> Tasks { get; set; }
}