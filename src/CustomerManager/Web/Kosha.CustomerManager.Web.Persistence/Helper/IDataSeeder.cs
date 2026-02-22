using System.Threading;
using System.Threading.Tasks;

namespace Kosha.CustomerManager.Web.Persistence.Helper;

internal interface IDataSeeder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task InvokeAsync(CancellationToken cancellation = default);
}