namespace Kosha.CustomerManager.Web.Models.Configurations;

public sealed class RouteConfiguration
{
    public const string Application = "api";

    public const string Separator = "/";

    public const string AreaName = "[area]";

    public const string ControllerName = "[controller]";

    public const string ActionName = "[action]";

    public const string IdName = "id";

    public const string IdRouteTemplate = "{" + IdName + ":guid}" ;

    public const string ApplicationRouteTemplate = Application + Separator + AreaName + Separator + ControllerName;
}