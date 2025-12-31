using System;

namespace Kosha.CustomerManager.Web.Domain.Helper;

public interface IModified 
{
    DateTime? Modified { get; set; }
}