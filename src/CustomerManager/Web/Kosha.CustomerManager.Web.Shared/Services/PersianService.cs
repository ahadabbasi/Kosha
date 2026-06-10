using System;
using System.Globalization;
using Kosha.CustomerManager.Web.Shared.Helper.Time;

namespace Kosha.CustomerManager.Web.Shared.Services;

internal sealed class PersianService(ITimeService service) : IPersianService
{
    private readonly PersianCalendar _calendar = new();

    public string Parse(DateTime dateTime) => 
        $"{_calendar.GetYear(dateTime):0000}/{_calendar.GetMonth(dateTime):00}/{_calendar.GetDayOfMonth(dateTime):00}";

    public string Today => Parse(service.Now);
}