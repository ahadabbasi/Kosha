using System;

namespace Kosha.CustomerManager.Web.Domain.Helper;

public interface IAudit : IRowVersion, IInserted
{
    Guid Id { get; set; }

    DateTime? Modified { get; set; }
}