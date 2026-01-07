using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Models.Customer;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Helper.Customer;

internal interface ICustomerContactService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Result<IEnumerable<CustomerContactTypeResponse>>> TypesAsync(CancellationToken cancellation = default);
}