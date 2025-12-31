namespace Kosha.CustomerManager.Web.Infrastructure.Configurations;

public class ServiceNameConfiguration
{
    /// <summary>
    /// Create task collector service name by type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string TaskCollectorServiceName(string type)
        => $"{type}TaskCollector";
}