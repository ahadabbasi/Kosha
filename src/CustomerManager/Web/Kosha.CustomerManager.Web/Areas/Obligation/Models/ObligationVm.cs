using System;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Models;

public record ObligationVm(Guid Id, string Title, string Description, DateTime Inserted);