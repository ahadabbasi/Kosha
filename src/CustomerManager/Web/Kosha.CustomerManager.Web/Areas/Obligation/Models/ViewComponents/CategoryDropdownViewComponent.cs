using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Obligation.Models.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Category;
using Kosha.CustomerManager.Web.Infrastructure.Models.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Models.ViewComponents;

public sealed class CategoryDropdownViewComponent(
    ICategoryService categoryService
) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        IEnumerable<CategoryResponse> categories =
            await categoryService.ListAsync();

        Guid? selected = null;

        if (
            Request.Query.TryGetValue(QueryConfiguration.Category, out StringValues value) && 
            value.Count != 0 && Guid.TryParse(value.First(), out Guid result)
        )
            selected = result;

        return View(
            new ObligationCategoryVm(
                selected,
                new ObligationCaptionVm[]{new (null, "نمایش همه")}
                    .Concat(categories
                        .Select(item => 
                            new ObligationCaptionVm(item.Id, item.Name)
                        )
                    )
            )
        );
    }
}