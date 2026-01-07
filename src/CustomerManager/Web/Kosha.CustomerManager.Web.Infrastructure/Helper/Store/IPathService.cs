using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Store;

public interface IPathService
{
    /// <summary>
    /// 
    /// </summary>
    string Separator { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<string>> DirectoryPathAsync(CancellationToken cancellation = default);
}