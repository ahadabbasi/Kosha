using System.ComponentModel.DataAnnotations;
using Kosha.CustomerManager.Web.Models.Configurations;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public class NewTagVm
{
    [
        Display(Name = DisplayNameConfiguration.Title),
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage)
    ]
    public string Title { get; set; }
};