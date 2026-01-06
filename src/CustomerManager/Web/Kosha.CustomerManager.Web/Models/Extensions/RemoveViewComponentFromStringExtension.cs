using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Models.Extensions;

public static class RemoveViewComponentFromStringExtension
{
    public static string RemoveViewComponentFromString(this string entry)
    {
        return entry.Replace(nameof(ViewComponent), string.Empty);
    }
}