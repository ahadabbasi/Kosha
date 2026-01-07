using Kosha.CustomerManager.Web.Infrastructure.Configurations.Options;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
using Microsoft.Extensions.Configuration;

namespace Kosha.CustomerManager.Web.Models.Infrastructure.Options;

internal sealed class BearerInformationConfigureOption(
    IConfiguration configuration
) : ConfigureOptions<BearerInformation>(configuration)
{
    internal const string Section = "Token";

    protected override string SectionName => Section;

}