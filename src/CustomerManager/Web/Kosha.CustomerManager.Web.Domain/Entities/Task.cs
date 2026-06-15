using System;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Domain.Helper;

namespace Kosha.CustomerManager.Web.Domain.Entities;

public class Task : IAudit
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string Type { get; set; }

    public string? Data { get; set; }

    public Guid? CustomerId { get; set; }

    public virtual Customer Customer { get; set; }

    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; }

    public DateTime? Modified { get; set; }

    public DateTime Inserted { get; set; }

    public byte[] RowVersion { get; set; }

    public virtual ICollection<TagTask> Tags { get; set; }

    public virtual ICollection<Action> Actions { get; set; }
}