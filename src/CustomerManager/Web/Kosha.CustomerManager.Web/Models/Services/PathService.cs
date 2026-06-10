using Kosha.CustomerManager.Web.Infrastructure.Helper.Store;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
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

    public string DirectoryPath =>
        string.Join(
            Separator,
            hostEnvironment.WebRootPath,
            Information.DirectoryName
        );
}