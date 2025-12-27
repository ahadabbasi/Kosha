using System.Linq;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Kosha.CustomerManager.Web.Models.Extensions;

public static class AddErrorExtension
{
    public static void AddError(this ModelStateDictionary modelState, Result result, Error? defaultError = null)
    {
        if (!result)
        {
            Error[] errors = [Error.None];

            if (defaultError != null)
            {
                errors = [defaultError];
            }

            if (result.Errors.Any())
                errors = result.Errors.ToArray();

            foreach (Error error in errors)
            {
                modelState.AddError(error);
            }
        }
    }

    public static void AddError(this ModelStateDictionary modelState, Error error)
    {
        modelState.AddModelError(
            error.Code,
            error.Message ?? string.Empty
        );
    }
}