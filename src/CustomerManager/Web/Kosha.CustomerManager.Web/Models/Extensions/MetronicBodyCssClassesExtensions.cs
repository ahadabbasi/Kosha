using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kosha.CustomerManager.Web.Models.Extensions;

public static class MetronicBodyCssClassesExtensions
{
    internal const string BodyCssClassesKey = "MetronicBodyCssClasses";

    public static void MetronicBodyCssClasses(this IHtmlHelper helper, params string[] classes)
    {
        helper.ViewData[BodyCssClassesKey] = string.Join(" ", classes);
    }

    public static IHtmlContent MetronicBodyCssClasses(this IHtmlHelper helper)
    {
        string value = string.Empty;

        if (helper.ViewData.TryGetValue(BodyCssClassesKey, out object? objectValue) && objectValue != null)
        {
            value = objectValue.ToString() ?? string.Empty;
        }

        return helper.Raw(value);
    }
}