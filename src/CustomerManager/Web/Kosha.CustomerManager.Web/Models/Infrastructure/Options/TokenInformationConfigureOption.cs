using Kosha.CustomerManager.Web.Infrastructure.Configurations.Options;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
using Microsoft.Extensions.Configuration;

namespace Kosha.CustomerManager.Web.Models.Infrastructure.Options;

internal sealed class TokenInformationConfigureOption(IConfiguration configuration) : ConfigureOptions<TokenInformation>(configuration)
{
    internal const string Section = "Token";

    protected override string SectionName => Section;

}