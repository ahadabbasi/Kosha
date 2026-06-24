using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Account.Models.ViewModels;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication.Models;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Infrastructure.Helper;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Account.Controllers.Applications;

[
    Area(AreaNameConfiguration.Account), ApiController,
    Route(RouteConfiguration.ApplicationRouteTemplate)
]
public sealed class TokenController(
    IUserService userService,
    IBearerAuthenticationMethodService authenticationMethodService
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Index([FromBody] LoginVm entry, CancellationToken cancellation)
    {
        IAuthenticationSignInResponse? response = null;

        IEnumerable<Error> errors = [ErrorConfiguration.UsernameOrPasswordIsWrong];

        Result<UserResponse> resultOfFind =
            await userService.FindByUsernameAsync(
                new UserRequest(entry.Username),
                cancellation
            );

        if (!resultOfFind)
            errors = resultOfFind.Errors;

        if (resultOfFind && resultOfFind.Data != null)
        {
            Result<IAuthenticationSignInResponse> resultOfVerified =
                await authenticationMethodService.SignInAsync(
                    new AuthenticationSignInRequest(
                        resultOfFind,
                        entry.Password,
                        true
                    ),
                    cancellation
                );

            if (!resultOfVerified)
                errors = resultOfVerified.Errors;

            if (resultOfVerified && resultOfVerified.Data != null)
                response = resultOfVerified.Data;
        }

        return response == null ? BadRequest(errors) : Ok(response);
    }
}