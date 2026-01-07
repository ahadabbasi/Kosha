using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Store;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Kosha.CustomerManager.Web.Models.Services;

internal sealed class PathService(
    IWebHostEnvironment hostEnvironment,
    IOptions<PathInformation> options
) : IPathService
{
    private PathInformation Information => options.Value;

    public string Separator => 
        System.IO.Path.DirectorySeparatorChar.ToString();

    public Task<Result<string>> DirectoryPathAsync(CancellationToken cancellation = default) => 
        Task.FromResult(
            Result.Success(
                string.Join(
                    Separator,
                    hostEnvironment.WebRootPath,
                    Information.DirectoryName
                )
            )
        );
}