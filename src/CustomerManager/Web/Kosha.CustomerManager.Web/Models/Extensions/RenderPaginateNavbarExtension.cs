using System;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Models.Paginate;
using Kosha.CustomerManager.Web.Models.ViewComponents;
using Kosha.CustomerManager.Web.Models.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace Kosha.CustomerManager.Web.Models.Extensions;

public static class RenderPaginateNavbarExtension
{
    extension<TData>(IHtmlHelper<TData> html) where TData : PaginateResponse
    {
        public async Task<IHtmlContent> RenderPaginateNavbarAsync(Func<int, string?> routeBuilder)
        {
            IHtmlContent result = html.Raw(string.Empty);

            try
            {
                IViewComponentHelper viewComponentHelper =
                    html.ViewContext.HttpContext.RequestServices
                        .GetRequiredService<IViewComponentHelper>();
                
                ((IViewContextAware)viewComponentHelper).Contextualize(html.ViewContext);

                result =
                    await viewComponentHelper.InvokeAsync(
                        nameof(PaginateViewComponent).RemoveViewComponentFromString(),
                        new CreatePaginateNavbarVm(
                            html.ViewData.Model, routeBuilder
                        )
                    );
            }
            catch (Exception)
            {
                //
            }

            return result;
        }
    }
}