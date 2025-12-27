using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Areas.Account.Models.ViewModels;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Hasher.Algorithms;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Infrastructure.Models.Hasher.Default;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Extensions;
using Kosha.CustomerManager.Web.Models.Infrastructure.Helper;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Areas.Account.Controllers.Applications;

[
    Area(AreaNameConfiguration.Account),
    Route(RouteConfiguration.ApplicationRouteTemplate),
    ApiController
]
public sealed class TokenController(
    IUserService authenticationService,
    IHasherService hasherService,
    ITokenService tokenService
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Index([FromBody] LoginVm entry, CancellationToken cancellation)
    {
        TokenResponse? response = null;

        Result<AuthenticationResponse> resultOfFind =
            await authenticationService.FindByUsernameAsync(
                entry,
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
            Result resultOfVerified =
                await hasherService.VerifyAsync(
                    new HasherRequest(entry.Password),
                    new HasherResponse(resultOfFind.Data.Password),
                    cancellation
                );

            if (resultOfVerified)
            {
                ModelState.Clear();

                IEnumerable<string> roles = [];

                Result<IEnumerable<string>> resultOfRoles =
                    await authenticationService.RolesAsync(
                        entry,
                        cancellation
                    );

                if (
                    resultOfRoles &&
                    resultOfRoles.Data != null &&
                    resultOfRoles.Data.Any()
                )
                {
                    roles = resultOfRoles.Data;
                }

                Result<TokenResponse> resultOfToken =
                    await tokenService.GenerateAsync(
                        resultOfFind,
                        roles,
                        cancellation
                    );

                if (
                    resultOfToken && 
                    resultOfToken.Data != null
                )
                {
                    ModelState.Clear();

                    response = resultOfToken.Data;
                }
            }
        }

        return !ModelState.IsValid || response == null ? BadRequest(ModelState) : Ok(response);
    }
}