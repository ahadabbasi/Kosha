using Kosha.CustomerManager.Web.Models.Infrastructure.Helper;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Obligation.Models.ViewComponents;

public sealed class AccessTokenViewComponent(ICookieAuthenticationMethodService authenticationService) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        IViewComponentResult result = Content(string.Empty);

        Result<string> resultToken = authenticationService.AccessToken();

        if (resultToken && !string.IsNullOrEmpty(resultToken.Data))
            result = View(new ObligationAccessTokenVm(resultToken.Data));

        return result;
    }
}