using System;

namespace Kosha.CustomerManager.Web.Domain.Helper;

public interface IAudit : IInserted, IModified, IRowVersion
{
    Guid Id { get; set; }
}