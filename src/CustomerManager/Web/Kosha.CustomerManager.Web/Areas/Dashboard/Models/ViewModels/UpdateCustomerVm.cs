using System;
using System.ComponentModel.DataAnnotations;
using Kosha.CustomerManager.Web.Infrastructure.Models.Customer;
using Kosha.CustomerManager.Web.Models.Configurations;

namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public class UpdateCustomerVm : CreateCustomerVm
{
    public UpdateCustomerVm()
    {
        
    }

    public UpdateCustomerVm(CustomerResponse response)
    {
        Id = response.Id;
        Name = response.Name;
        Family = response.Family;
    }

    [Required(ErrorMessage = ErrorMessageConfiguration.DoNotChangeValue)]
    public Guid Id { get; set; }
}