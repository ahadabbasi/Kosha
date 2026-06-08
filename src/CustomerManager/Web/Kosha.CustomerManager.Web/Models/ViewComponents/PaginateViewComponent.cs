using Kosha.CustomerManager.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Models.ViewComponents;

public sealed class PaginateViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(CreatePaginateNavbarVm entry)
    {
        int start = 1;
        int end = entry.Paginate.TotalPages;

        if (entry.Paginate.TotalPages > 5)
        {
            end = 5;

            if (entry.Paginate.Page >= 3)
            {
                start = entry.Paginate.Page - 2;
                end = entry.Paginate.Page + 2;

                if (entry.Paginate.Page >= entry.Paginate.TotalPages - 2)
                {
                    start = entry.Paginate.TotalPages - 4;
                    end = entry.Paginate.TotalPages;
                }
            }
        }

        return 
            entry.Paginate.Page == entry.Paginate.TotalPages ? 
                Content(string.Empty) :
                View(
                    new NavbarVm(
                        start, 
                        end, 
                        entry.Paginate.Page, 
                        entry.LinkGenerator
                    )
                );
    }
}