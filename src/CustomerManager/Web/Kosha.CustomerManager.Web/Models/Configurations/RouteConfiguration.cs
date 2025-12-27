namespace Kosha.CustomerManager.Web.Models.Configurations;

public sealed class RouteConfiguration
{
    public const string Application = "api";

    public const string Separator = "/";

    public const string ApplicationRouteTemplate = Application + Separator + "[area]" + Separator + "[controller]";
}