using Kosha.CustomerManager.Web.Infrastructure.Models.File;
using Microsoft.Extensions.Configuration;

namespace Kosha.CustomerManager.Web.Infrastructure.Configurations.Options;

internal class FileInformationConfigureOption(
    IConfiguration configuration
) : ConfigureOptions<FileInformation>(configuration)
{
    protected override string SectionName => "File";
}