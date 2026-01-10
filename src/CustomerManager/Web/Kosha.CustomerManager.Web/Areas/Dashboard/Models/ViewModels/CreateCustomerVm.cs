using System.ComponentModel.DataAnnotations;
using Kosha.CustomerManager.Web.Models.Configurations;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public class CreateCustomerVm
{
    [
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage),
        Display(Name = DisplayNameConfiguration.Name)
    ]
    public string Name { get; set; } = string.Empty;

    [
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage),
        Display(Name = DisplayNameConfiguration.Family)
    ]
    public string Family { get; set; } = string.Empty;
}