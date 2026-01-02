using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kosha.CustomerManager.Web.Models.Extensions;

public static class PageTitleExtension
{
    internal const string PageTitleKey = "PageTitle";

    public static void PageTitle(this IHtmlHelper helper, string title)
    {
        helper.ViewData[PageTitleKey] = title;
    }


    public static IHtmlContent PageTitle(this IHtmlHelper helper)
    {
        return helper.Raw(helper.ViewData[PageTitleKey] as string ?? string.Empty);
    }
}