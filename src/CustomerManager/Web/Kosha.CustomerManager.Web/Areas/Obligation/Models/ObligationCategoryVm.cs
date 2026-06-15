using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Models;

public record ObligationCategoryVm(
    Guid? Selected = null,
    IEnumerable<ObligationCaptionVm>? Options = null
) : IEnumerable<ObligationCaptionVm>
{
    public IEnumerator<ObligationCaptionVm> GetEnumerator() => 
        (Options ?? Enumerable.Empty<ObligationCaptionVm>()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}