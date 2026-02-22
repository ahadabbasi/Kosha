using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Account.Models.ViewModels;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication.Models;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Extensions;
using Kosha.CustomerManager.Web.Models.Infrastructure.Helper;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Account.Controllers.Applications;

[
    Area(AreaNameConfiguration.Account),
    Route(RouteConfiguration.ApplicationRouteTemplate),
    ApiController
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

        Result<UserResponse> resultOfFind =
            await userService.FindByUsernameAsync(
                new UserRequest(entry.Username),
                cancellation
            );

        ModelState.AddError(ErrorConfiguration.UsernameOrPasswordIsWrong);

        if (!resultOfFind)
        {
            ModelState.Clear();
            ModelState.AddError(resultOfFind);
        }

        if (
            resultOfFind &&
            resultOfFind.Data != null
        )
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

            if (resultOfVerified && resultOfVerified.Data != null)
            {
                ModelState.Clear();

                response = resultOfVerified.Data;
            }
        }

        return !ModelState.IsValid || response == null ? BadRequest(ModelState) : Ok(response);
    }
}