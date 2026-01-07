using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Store;

public interface IFileService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<string>> ReadContentAsync(string fileName, CancellationToken cancellation = default);
}