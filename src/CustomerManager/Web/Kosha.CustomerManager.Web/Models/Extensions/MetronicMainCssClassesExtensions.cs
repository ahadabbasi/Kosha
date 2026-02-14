using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kosha.CustomerManager.Web.Models.Extensions;

public static class MetronicMainCssClassesExtensions
{
    internal const string MainCssClassesKey = "MetronicMainCssClasses";

    public static void MetronicMainCssClasses(this IHtmlHelper helper, params string[] classes)
    {
        helper.ViewData[MainCssClassesKey] = string.Join(" ", classes);
    }

    public static IHtmlContent MetronicMainCssClasses(this IHtmlHelper helper)
    {
        string value = string.Empty;

        if (helper.ViewData.TryGetValue(MainCssClassesKey, out object? objectValue) && objectValue != null)
        {
            value = objectValue.ToString() ?? string.Empty;
        }

        return helper.Raw(value);
    }
}