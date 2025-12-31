using System;
using Kosha.CustomerManager.Web.Domain.Helper;

namespace Kosha.CustomerManager.Web.Domain.Entities;

public class TagTask : IInserted, IRowVersion
{
    public Guid TagId { get; set; }

    public virtual Tag Tag { get; set; }

    public Guid TaskId { get; set; }

    public virtual Task Task { get; set; }

    public DateTime Inserted { get; set; }

    public byte[] RowVersion { get; set; }
}