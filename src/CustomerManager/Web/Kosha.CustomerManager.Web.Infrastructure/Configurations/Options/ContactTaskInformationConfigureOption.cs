using Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;
using Microsoft.Extensions.Configuration;

namespace Kosha.CustomerManager.Web.Infrastructure.Configurations.Options;

internal class ContactTaskInformationConfigureOption(
    IConfiguration configuration
) : ConfigureOptions<ContactTaskInformation>(configuration)
{
    protected override string SectionName => "ContactTask";
}