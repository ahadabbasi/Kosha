using System;
using System.Collections;
using System.Collections.Generic;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public sealed record NavbarVm(
    int Start,
    int End,
    int Current,
    Func<int, string?> LinkGenerator
) : IEnumerable<int>
{
    private IList<int>? Pages { get; set; }

    public IEnumerator<int> GetEnumerator()
    {
        if (Pages == null || Pages.Count == 0)
        {
            Pages ??= new List<int>();

            for (int index = Start; index <= End; index++)
                Pages.Add(index);
        }

        return Pages.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() 
        => GetEnumerator();
}