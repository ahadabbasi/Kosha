using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Models.ViewComponents;

public sealed class UserViewComponent(IAuthorizeService authorizeService) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        Result<AuthorizationResponse> resultAuthenticate = 
            authorizeService.Authenticate();

        return resultAuthenticate && resultAuthenticate.Data is not null
            ? View(resultAuthenticate.Data)
            : Content(string.Empty);
    }
}