using Kosha.CustomerManager.Web.Infrastructure.Configurations.Options;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
using Microsoft.Extensions.Configuration;

namespace Kosha.CustomerManager.Web.Models.Infrastructure.Options;

internal sealed class PathInformationConfigureOption(
    IConfiguration configuration
) : ConfigureOptions<PathInformation>(configuration)
{
    protected override string SectionName => "Path";
}