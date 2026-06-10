using System;

namespace Kosha.CustomerManager.Web.Shared.Helper.Time;

public interface IPersianService
{
    /// <summary>
    /// Convert given date time to persian date time string with format of "yyyy/MM/dd"
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    string Parse(DateTime dateTime);

    /// <summary>
    /// Get current date time on utc and convert it to persian date time string with format of "yyyy/MM/dd"
    /// </summary>
    string Today { get; }
}