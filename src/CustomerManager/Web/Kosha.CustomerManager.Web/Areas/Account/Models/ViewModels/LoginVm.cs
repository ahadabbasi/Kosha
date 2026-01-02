using System.ComponentModel.DataAnnotations;
using Kosha.CustomerManager.Web.Models.Configurations;

namespace Kosha.CustomerManager.Web.Areas.Account.Models.ViewModels;

public class LoginVm
{
    [
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage), 
        Display(Name = DisplayNameConfiguration.Username)
    ]
    public string Username { get; set; }

    [
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage),
        Display(Name = DisplayNameConfiguration.Password)
    ]
    public string Password { get; set; }

    [
        Display(Name = DisplayNameConfiguration.RememberMe)
    ] 
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}