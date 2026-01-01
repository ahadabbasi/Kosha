using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Shared.Helper.Hasher.Models;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Shared.Helper.Hasher;

public interface IHasherService<in TRequest, TResponse>
    where TRequest : class, IHasherRequest
    where TResponse : class, IHasherResponse
{
    /// <summary>
    /// Hash the given request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<TResponse>> HashAsync(TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// verify the given request with the given hash
    /// </summary>
    /// <param name="request"></param>
    /// <param name="hashed"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result> VerifyAsync(TRequest request, TResponse hashed, CancellationToken cancellationToken = default);
}