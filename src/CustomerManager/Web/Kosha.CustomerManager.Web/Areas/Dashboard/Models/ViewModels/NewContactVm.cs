using System;
using System.ComponentModel.DataAnnotations;
using Kosha.CustomerManager.Web.Infrastructure.Configurations;
using Kosha.CustomerManager.Web.Infrastructure.Models.Customer;
using Kosha.CustomerManager.Web.Models.Configurations;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public class NewContactVm
{
    public NewContactVm()
    {
        
    }

    public NewContactVm(CustomerResponse customer)
    {
        Customer = customer.Id;
        CustomerName = customer.Name;
        CustomerFamily = customer.Family;
    }

    [Required(ErrorMessage = ErrorMessageConfiguration.DoNotChangeValue)]
    public Guid Customer { get; set; }

    [Required(ErrorMessage = ErrorMessageConfiguration.DoNotChangeValue)]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = ErrorMessageConfiguration.DoNotChangeValue)]
    public string CustomerFamily { get; set; } = string.Empty;

    [
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage),
        Display(Name = DisplayNameConfiguration.Type)
    ]
    public string Type { get; set; }

    [
        Required(ErrorMessage = ErrorMessageConfiguration.RequiredErrorMessage),
        Display(Name = DisplayNameConfiguration.Value)
    ]
    public string Value { get; set; }
}