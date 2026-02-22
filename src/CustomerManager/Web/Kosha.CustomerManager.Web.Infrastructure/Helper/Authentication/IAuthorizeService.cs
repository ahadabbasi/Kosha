using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;

public interface IAuthorizeService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Result IsAuthenticate();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Result<AuthorizationResponse> Authenticate();
}