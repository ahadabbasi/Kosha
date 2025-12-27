using System;

namespace Kosha.CustomerManager.Web.Shared.Helper.Time;

public interface ITimeService
{
    /// <summary>
    /// Get current date time on utc
    /// </summary>
    DateTime Now { get; }

    /// <summary>
    /// Get current date on utc
    /// </summary>
    DateTime Today { get; }

    /// <summary>
    /// Get current date time on local
    /// </summary>
    DateTime Local { get; }
}