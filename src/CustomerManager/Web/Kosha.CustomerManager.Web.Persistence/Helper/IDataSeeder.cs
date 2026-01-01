using System.Threading;
using System.Threading.Tasks;

namespace Kosha.CustomerManager.Web.Persistence.Helper;

public interface IDataSeeder
{
    Task InvokeAsync(CancellationToken cancellation = default);
}