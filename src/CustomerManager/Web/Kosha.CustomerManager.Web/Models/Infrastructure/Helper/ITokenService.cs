using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Models.Infrastructure.Helper;

public interface ITokenService
{
    /// <summary>
    /// Generate token for user
    /// </summary>
    /// <param name="request"></param>
    /// <param name="roles"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<TokenResponse>> GenerateAsync(AuthenticationResponse request, IEnumerable<string>? roles = null, CancellationToken cancellation = default);
}