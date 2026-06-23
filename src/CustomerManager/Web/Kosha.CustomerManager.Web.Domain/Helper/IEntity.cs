using System;

namespace Kosha.CustomerManager.Web.Domain.Helper;

public interface IEntity : IRowVersion
{
    Guid Id { get; set; }
}