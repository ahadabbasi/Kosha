using System.Collections.Generic;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Models;

public sealed record ObligationDetailsVm(string Title, IEnumerable<ObligationDetailsInformationVm> Information);