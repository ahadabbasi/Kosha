using Kosha.CustomerManager.Web.Infrastructure.Helper.Authentication;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Models.Infrastructure.Helper;

public interface ICookieAuthenticationMethodService : IAuthenticationMethodService
{
    Result<string> AccessToken();
}