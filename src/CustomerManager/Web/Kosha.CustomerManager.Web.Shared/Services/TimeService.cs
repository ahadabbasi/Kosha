using System;
using Kosha.CustomerManager.Web.Shared.Helper.Time;

namespace Kosha.CustomerManager.Web.Shared.Services;

internal sealed class TimeService : ITimeService
{
    public DateTime Now => DateTime.UtcNow;

    public DateTime Today => Now.Date;

    public DateTime Local => DateTime.Now;
}