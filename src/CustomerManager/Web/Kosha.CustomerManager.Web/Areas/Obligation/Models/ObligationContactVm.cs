using System.Collections.Generic;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Models;

public sealed record ObligationContactVm(
    string Name, 
    string Family, 
    IEnumerable<ObligationContactInformationVm>? Information = null
);