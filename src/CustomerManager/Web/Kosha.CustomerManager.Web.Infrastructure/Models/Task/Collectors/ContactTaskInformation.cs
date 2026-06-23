namespace Kosha.CustomerManager.Web.Infrastructure.Models.Task.Collectors;

internal sealed class ContactTaskInformation
{
    public string UnknownOrganization { get; set; } = string.Empty;

    public string DefaultDescription { get; set; } = string.Empty;

    public ContactTaskCaptionInformation Caption { get; set; } = new();

}