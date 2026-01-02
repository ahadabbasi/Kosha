using System.ComponentModel.DataAnnotations;
using Kosha.CustomerManager.Web.Models.Configurations;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public sealed class CreateUserVm : ProfileUpdateVm
{
    [
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage),
        Display(Name = DisplayNameConfiguration.Password)
    ]
    public string Password { get; set; }

    [
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage), 
        Compare(nameof(Password), ErrorMessage = ErrorMessageConfiguration.CompareErrorMessage),
        Display(Name = DisplayNameConfiguration.ConfirmPassword)
    ]
    public string ConfirmPassword { get; set; }

}