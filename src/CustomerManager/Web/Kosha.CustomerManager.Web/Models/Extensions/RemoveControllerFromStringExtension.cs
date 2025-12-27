using Microsoft.AspNetCore.Mvc;

namespace Kosha.CustomerManager.Web.Models.Extensions;

public static class RemoveControllerFromStringExtension
{
    public static string RemoveControllerFromString(this string controllerName)
    {
        return controllerName.EndsWith(nameof(Controller)) 
            ? controllerName[..^nameof(Controller).Length] 
            : controllerName;
    }
}