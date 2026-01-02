using System.ComponentModel.DataAnnotations;
using Kosha.CustomerManager.Web.Models.Configurations;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public class ProfileUpdateVm
{
    [
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage),
        Display(Name = DisplayNameConfiguration.Username)
    ]
    public virtual string Username { get; set; }

    [Display(Name = DisplayNameConfiguration.Name)]
    public virtual string? Name { get; set; }

    [Display(Name = DisplayNameConfiguration.Family)]
    public virtual string? Family { get; set; }

    [
        Display(Name = DisplayNameConfiguration.PhoneNumber),
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage)
    ]
    public virtual string PhoneNumber { get; set; }

}