using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Customer;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Store;
using Kosha.CustomerManager.Web.Infrastructure.Models.Customer;
using Kosha.CustomerManager.Web.Shared.Results;

namespace Kosha.CustomerManager.Web.Infrastructure.Services.Customer;

internal sealed class CustomerContactService(
    IFileService fileService,
    IPathService pathService
) : ICustomerContactService
{
    public async Task<Result<IEnumerable<CustomerContactTypeResponse>>> TypesAsync(CancellationToken cancellation = default)
    {
        Result<string> resultFile = 
            await fileService.ReadContentAsync(
                string.Join(
                    pathService.Separator,
                    "Customer",
                    "Types"
                ),
                cancellation
            );

        IDictionary<string, string>? types = null;

        if (
            resultFile && 
            !string.IsNullOrEmpty(resultFile.Data)
        )
        {
            try
            {
                types =
                    JsonSerializer.Deserialize<Dictionary<string, string>>(
                        resultFile.Data
                    );
            }
            catch
            {
                //
            }
        }

        return
            types is null || types.Count == 0
                ? Result.Failed<IEnumerable<CustomerContactTypeResponse>>(Error.None)
                : Result.Success(
                    types.Select(pair => new CustomerContactTypeResponse(pair.Key, pair.Value))
                );
    }
}