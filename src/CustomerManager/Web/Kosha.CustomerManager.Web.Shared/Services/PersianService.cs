using System;
using System.Globalization;
using Kosha.CustomerManager.Web.Shared.Helper.Time;

namespace Kosha.CustomerManager.Web.Shared.Services;

internal sealed class PersianService(ITimeService service) : IPersianService
{
    public string ConvertToPersianDateTime(DateTime dateTime)
    {
        PersianCalendar calendar = new();
        return $"{calendar.GetYear(dateTime):0000}/{calendar.GetMonth(dateTime):00}/{calendar.GetDayOfMonth(dateTime):00}";
    }

    public string Today => ConvertToPersianDateTime(service.Now);
}