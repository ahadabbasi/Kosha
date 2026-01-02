using System.ComponentModel.DataAnnotations;
using Kosha.CustomerManager.Web.Models.Configurations;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public sealed class ChangePasswordVm
{
    [
        Display(Name = DisplayNameConfiguration.OldPassword),
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage)
    ]
    public string OldPassword { get; set; }

    [
        Display(Name = DisplayNameConfiguration.Password),
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage)
    ]
    public string NewPassword { get; set; }

    [
        Display(Name = DisplayNameConfiguration.ConfirmPassword),
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage),
        Compare(nameof(NewPassword), ErrorMessage = ErrorMessageConfiguration.CompareErrorMessage)
    ]
    public string ConfirmNewPassword { get; set; }
}